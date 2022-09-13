using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Dto;
using TUKD.API.Interface;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Repository
{
    public class GolonganRepo : Repo<Golongan>, IGolonganRepo
    {
        public GolonganRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<PrimengTableResult<Golongan>> Paging(PrimengTableParam<GolonganGet> param)
        {
            PrimengTableResult<Golongan> Result = new PrimengTableResult<Golongan>();
            IQueryable<Golongan> query = (
                from data in _tukdContext.Golongan
                select new Golongan
                {
                    Idgol = data.Idgol,
                    Kdgol = data.Kdgol,
                    Nmgol = data.Nmgol,
                    Pangkat = data.Pangkat
                }
                ).AsQueryable();
            if(param.Parameters.Kdgol != "x")
            {
                query = query.Where(w => w.Kdgol.Trim() == param.Parameters.Kdgol.Trim()).AsQueryable();
            }
            if (!String.IsNullOrEmpty(param.GlobalFilter))
            {
                query = query.Where(w =>
                    EF.Functions.Like(w.Kdgol.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Nmgol.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Pangkat.Trim(), "%" + param.GlobalFilter + "%")
                ).AsQueryable();
            }
            if (!String.IsNullOrEmpty(param.SortField))
            {
                if (param.SortField == "kdgol")
                {
                    if (param.SortOrder > 0)
                    {
                        query = query.OrderBy(o => o.Kdgol).AsQueryable();
                    }
                    else
                    {
                        query = query.OrderByDescending(o => o.Kdgol).AsQueryable();
                    }
                }
                else if (param.SortField == "nmgol")
                {
                    if (param.SortOrder > 0)
                    {
                        query = query.OrderBy(o => o.Nmgol).AsQueryable();
                    }
                    else
                    {
                        query = query.OrderByDescending(o => o.Nmgol).AsQueryable();
                    }
                }
                else if (param.SortField == "pangkat")
                {
                    if (param.SortOrder > 0)
                    {
                        query = query.OrderBy(o => o.Pangkat).AsQueryable();
                    }
                    else
                    {
                        query = query.OrderByDescending(o => o.Pangkat).AsQueryable();
                    }
                }
            }
            Result.Data = await query.Skip(param.Start).Take(param.Rows).ToListAsync();
            Result.Totalrecords = await query.CountAsync();
            return Result;
        }

        public async Task<bool> Update(Golongan param)
        {
            Golongan data = await _tukdContext.Golongan.Where(w => w.Idgol == param.Idgol).FirstOrDefaultAsync();
            if (data == null)
                return false;
            data.Kdgol = param.Kdgol;
            data.Nmgol = param.Nmgol;
            data.Pangkat = param.Pangkat;
            _tukdContext.Golongan.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<Golongan> ViewData(long Idgol)
        {
            Golongan Result = await (
               from data in _tukdContext.Golongan
               select new Golongan
               {
                   Idgol = data.Idgol,
                   Kdgol = data.Kdgol,
                   Nmgol = data.Nmgol,
                   Pangkat = data.Pangkat
               }
               ).FirstOrDefaultAsync();
            return Result;
        }

        public async Task<List<Golongan>> ViewDatas(GolonganGet param)
        {
            List<Golongan> Result = new List<Golongan>();
            IQueryable<Golongan> query = (
                from data in _tukdContext.Golongan
                select new Golongan
                {
                    Idgol = data.Idgol,
                    Kdgol = data.Kdgol,
                    Nmgol = data.Nmgol,
                    Pangkat = data.Pangkat
                }
                ).AsQueryable();
            if (param.Kdgol != "x")
            {
                query = query.Where(w => w.Kdgol.Trim() == param.Kdgol.Trim()).AsQueryable();
            }
            Result = await query.ToListAsync();
            return Result;
        }
    }
}
