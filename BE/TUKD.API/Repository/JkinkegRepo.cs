using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Dto;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class JkinkegRepo : Repo<Jkinkeg>, IJkinkegRepo
    {
        public JkinkegRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<PrimengTableResult<Jkinkeg>> Paging(PrimengTableParam<Jkinkeg> param)
        {
            PrimengTableResult<Jkinkeg> Result = new PrimengTableResult<Jkinkeg>();
            IQueryable<Jkinkeg> Query = _tukdContext.Jkinkeg.AsQueryable();
            if (!String.IsNullOrEmpty(param.GlobalFilter))
            {
                Query = Query.Where(w =>
                    EF.Functions.Like(w.Urjkk.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Kdjkk.Trim(), "%" + param.GlobalFilter + "%")
                ).AsQueryable();
            }
            if (!String.IsNullOrEmpty(param.SortField))
            {
                if (param.SortField == "kdjkk")
                {
                    if (param.SortOrder > 0)
                    {
                        Query = Query.OrderBy(o => o.Kdjkk).AsQueryable();
                    }
                    else
                    {
                        Query = Query.OrderByDescending(o => o.Kdjkk).AsQueryable();
                    }
                }
                else if (param.SortField == "urjkk")
                {
                    if (param.SortOrder > 0)
                    {
                        Query = Query.OrderBy(o => o.Urjkk).AsQueryable();
                    }
                    else
                    {
                        Query = Query.OrderByDescending(o => o.Urjkk).AsQueryable();
                    }
                }
            }
            Result.Data = await Query.Skip(param.Start).Take(param.Rows).OrderBy(o => o.Kdjkk).ToListAsync();
            Result.Totalrecords = await Query.CountAsync();
            return Result;
        }

        public async Task<bool> Update(Jkinkeg param)
        {
            Jkinkeg data = await _tukdContext.Jkinkeg.Where(w => w.Kdjkk.Trim() == param.Kdjkk.Trim()).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Urjkk = param.Urjkk;
            _tukdContext.Jkinkeg.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0) return true;
            return false;
        }
    }
}
