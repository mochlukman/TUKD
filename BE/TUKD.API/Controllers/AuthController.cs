using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RKPD.API.Helpers;
using TUKD.API.Interface;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Controllers
{
    [Route("api/[controller]"), AllowAnonymous]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUow _uow;
        private readonly TukdContext _context;
        public AuthController(IConfiguration configuration, IUow uow, TukdContext tukdContext)
        {
            _config = configuration;
            _uow = uow;
            _context = tukdContext;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoginParam param)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                Webuser user = await _uow.WebuserRepo.ViewData(param.UserId.Trim());
                if(user == null)
                {
                    return BadRequest("Login Gagal");
                }
                if (!String.IsNullOrEmpty(user.Blokid.ToString()))
                {
                    if (user.Blokid >= 3)
                    {
                        return BadRequest("User Terblokir, Hubungi Administrator Untuk Membuka Kembali");
                    }
                }
                if (Hashing.Check(param.Pwd, user.Pwd))
                {
                    return Ok(new { token = CreateToken(user, param.kdTahun) });
                }
                else
                {
                    if (user.Groupid != 1)
                    {
                        _uow.WebuserRepo.UpdateBlokId(user.Userid);
                    }
                    return BadRequest("Login Gagal");
                }
            } catch(Exception e)
            {
                ModelState.AddModelError("error", e.InnerException?.Message ?? e.Message);
                return BadRequest(ModelState);
            }
        }
        private string CreateToken(Webuser user, string KdTahun)
        {
            var nmtahun = _uow.TahunRepo.GetNamaTahun(KdTahun);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Userid.Trim()),
                new Claim(ClaimTypes.GivenName, user.Nama ?? user.Userid.Trim()),
                new Claim("KdTahap", !String.IsNullOrEmpty(user.Kdtahap) ? user.Kdtahap.Trim() : ""),
                new Claim("GroupId", !String.IsNullOrEmpty(user.Groupid.ToString()) ? user.Groupid.ToString() : "0"),
                new Claim("Kdgroup", !String.IsNullOrEmpty(user.Group.Kdgroup) ? user.Group.Kdgroup.Trim() : ""),
                new Claim("KdTahun", KdTahun),
                new Claim("NmTahun", nmtahun),
                new Claim("RoleName", user.Group != null ? user.Group.Nmgroup.Trim() : ""),
                new Claim("Idunit", user.IdunitNavigation != null ? user.IdunitNavigation.Idunit.ToString() : "0"),
                new Claim("Kdunit", user.IdunitNavigation != null ? user.IdunitNavigation.Kdunit.Trim() : ""),
                new Claim("Nmunit", user.IdunitNavigation != null ? user.IdunitNavigation.Nmunit.Trim() : ""),
                new Claim("Insert", user.Stinsert.ToString()),
                new Claim("Update", user.Stupdate.ToString()),
                new Claim("Delete", user.Stdelete.ToString()),
                new Claim("IniPakEndul", user.Userid == "pakendul" ? "true" : "false"),
                new Claim("Stmaker", user.Stmaker.ToString()),
                new Claim("Stchecker", user.Stchecker.ToString()),
                new Claim("Staproval", user.Staproval.ToString()),
                new Claim("Stlegitimator", user.Stlegitimator.ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Token:Key").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        [Authorize]
        [HttpGet]
        public ActionResult<String> ReadToken()
        {
            var user = User.Claims.FirstOrDefault(x => x.Type.Equals("UnitKey", StringComparison.InvariantCultureIgnoreCase));
            return Ok(user.Value);
        }
    }
}