using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using Swashbuckle.AspNetCore.SwaggerUI;
using RKPD.API.Hubs;
using TUKD.API.Interface;
using TUKD.API.Repository;
using TUKD.API.Models;
using RKPD.API.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;

namespace TUKD.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<TukdContext>((provider, options) =>
            {
                var httpContext = provider.GetService<IHttpContextAccessor>().HttpContext;
                var suffix = httpContext?.Request.Headers["tahun-suffix"];
                options.UseSqlServer(ConfigurationExtension.SetSuffix(Configuration, suffix));
            });
            services.AddScoped<DbConnection>(provider =>
            {
                var httpContext = provider.GetService<IHttpContextAccessor>().HttpContext;
                var suffix = httpContext?.Request.Headers["tahun-suffix"];
                return new SqlConnection(ConfigurationExtension.SetSuffix(Configuration, suffix));
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",
                    builder => builder.AllowAnyOrigin());
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("Token:Key").Value)),
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
               });
            services.AddAuthorization(Options =>
            {
                Options.AddPolicy("Administrator", x => x.RequireClaim("GroupId", "1_"));
            });
            services.AddMvc(opt =>
            {
                var policy = new AuthorizationPolicyBuilder()
                  .RequireAuthenticatedUser()
                  .Build();

                opt.Filters.Add(new AuthorizeFilter(policy));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(c => {
                c.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                c.SerializerSettings.Formatting = Formatting.Indented;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "TUKD",
                    Description = "TUKD API",
                    Version = "v1",
                    Contact = new Contact
                    {
                        Name = "Irfan Muslim Saputra",
                        Email = "irfanmuslimsaputra@gmail.com"
                    }
                });
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { } }
                };
                c.AddSecurityDefinition("Bearer",
                   new ApiKeyScheme
                   {
                       In = "header",
                       Description = "Masukan JWT Token",
                       Name = "Authorization",
                       Type = "apiKey"
                   });
                c.AddSecurityRequirement(security);

            });
            services.AddTransient<IUow, Uow>();
            services.AddAutoMapper();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseAuthentication();
            //app.UseRequestResponseLogging();
            //app.UseMiddleware<SerilogRequestLogger>();
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            //seeder.Seed().Wait();
            app.UseSignalR(r => r.MapHub<NotificationR>("/notif"));
            app.UseMvc(r => {
                r.MapRoute(
                    name: "default",
                    template: "{controller=Router}/{action=Index}"
                    );
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Core API");
                c.DocumentTitle = "TUKD API Request";
                c.DocExpansion(DocExpansion.None);
            });
        }
    }
}
