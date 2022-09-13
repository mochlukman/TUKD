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
    public class DaftunitRepo : Repo<Daftunit>, IDaftunitRepo
    {
        public DaftunitRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<PrimengTableResult<DaftunitView>> Paging(PrimengTableParam<DaftunitGet> param)
        {
            PrimengTableResult<DaftunitView> Result = new PrimengTableResult<DaftunitView>();
            IQueryable<DaftunitView> query = (
                from data in _tukdContext.Daftunit
                join urusan in _tukdContext.Dafturus on data.Idurus equals urusan.Idurus into urusanMatch from urusan_data in urusanMatch.DefaultIfEmpty()
                join struunit in _tukdContext.Struunit on data.Kdlevel equals struunit.Kdlevel into struunitMatch from struunit_data in struunitMatch.DefaultIfEmpty() 
                select new DaftunitView
                {
                    Akrounit = data.Akrounit,
                    Alamat = data.Alamat,
                    Datecreate = data.Datecreate,
                    Dateupdate = data.Dateupdate,
                    Idpemda = data.Idpemda,
                    Idunit = data.Idunit,
                    Idurus = data.Idurus,
                    Kdlevel = data.Kdlevel,
                    Kdunit = data.Kdunit,
                    Nmunit = data.Nmunit,
                    Staktif = data.Staktif,
                    Telepon = data.Telepon,
                    Type = data.Type,
                    IdurusNavigation = urusan_data ?? null,
                    KdlevelNavigation = struunit_data ?? null
                }).AsQueryable();
            if (param.Parameters.Kdlevel.ToString() != "0")
            {
                query = query.Where(w => w.Kdlevel == param.Parameters.Kdlevel).AsQueryable();
            }
            if (!String.IsNullOrEmpty(param.GlobalFilter))
            {
                query = query.Where(w =>
                    EF.Functions.Like(w.Kdunit.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Nmunit.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Type.Trim(), "%" + param.GlobalFilter + "%")
                ).AsQueryable();
            }
            if (!String.IsNullOrEmpty(param.SortField))
            {
                if (param.SortField == "kdunit")
                {
                    if (param.SortOrder > 0)
                    {
                        query = query.OrderBy(o => o.Kdunit).AsQueryable();
                    }
                    else
                    {
                        query = query.OrderByDescending(o => o.Kdunit).AsQueryable();
                    }
                }
                else if (param.SortField == "nmunit")
                {
                    if (param.SortOrder > 0)
                    {
                        query = query.OrderBy(o => o.Nmunit).AsQueryable();
                    }
                    else
                    {
                        query = query.OrderByDescending(o => o.Nmunit).AsQueryable();
                    }
                }
                else if (param.SortField == "type")
                {
                    if (param.SortOrder > 0)
                    {
                        query = query.OrderBy(o => o.Type).AsQueryable();
                    }
                    else
                    {
                        query = query.OrderByDescending(o => o.Type).AsQueryable();
                    }
                }
            }
            Result.Data = await query.Skip(param.Start).Take(param.Rows).ToListAsync();
            Result.Totalrecords = await query.CountAsync();
            return Result;
        }

        public async Task<bool> Update(Daftunit param)
        {
            Daftunit data = await _tukdContext.Daftunit.Where(w => w.Idunit == param.Idunit).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Dateupdate = param.Dateupdate;
            data.Kdunit = param.Kdunit;
            data.Nmunit = param.Nmunit;
            data.Akrounit = param.Akrounit;
            data.Kdlevel = param.Kdlevel;
            data.Alamat = param.Alamat;
            data.Telepon = param.Telepon;
            data.Type = param.Type;
            _tukdContext.Daftunit.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0) return true;
            return false;
        }

        public async Task<DaftunitView> ViewData(long Idunit)
        {
            DaftunitView Result = await (
                from data in _tukdContext.Daftunit
                join urusan in _tukdContext.Dafturus on data.Idurus equals urusan.Idurus into urusanMatch
                from urusan_data in urusanMatch.DefaultIfEmpty()
                join struunit in _tukdContext.Struunit on data.Kdlevel equals struunit.Kdlevel into struunitMatch
                from struunit_data in struunitMatch.DefaultIfEmpty()
                where data.Idunit == Idunit
                select new DaftunitView
                {
                    Akrounit = data.Akrounit,
                    Alamat = data.Alamat,
                    Datecreate = data.Datecreate,
                    Dateupdate = data.Dateupdate,
                    Idpemda = data.Idpemda,
                    Idunit = data.Idunit,
                    Idurus = data.Idurus,
                    Kdlevel = data.Kdlevel,
                    Kdunit = data.Kdunit,
                    Nmunit = data.Nmunit,
                    Staktif = data.Staktif,
                    Telepon = data.Telepon,
                    Type = data.Type,
                    IdurusNavigation = urusan_data ?? null,
                    KdlevelNavigation = struunit_data ?? null
                }).FirstOrDefaultAsync();
            return Result;
        }

        public async Task<List<DaftunitView>> ViewDatas(DaftunitGet param)
        {
            List<DaftunitView> Result = new List<DaftunitView>();
            IQueryable<DaftunitView> query = (
                from data in _tukdContext.Daftunit
                join urusan in _tukdContext.Dafturus on data.Idurus equals urusan.Idurus into urusanMatch
                from urusan_data in urusanMatch.DefaultIfEmpty()
                join struunit in _tukdContext.Struunit on data.Kdlevel equals struunit.Kdlevel into struunitMatch
                from struunit_data in struunitMatch.DefaultIfEmpty()
                select new DaftunitView
                {
                    Akrounit = data.Akrounit,
                    Alamat = data.Alamat,
                    Datecreate = data.Datecreate,
                    Dateupdate = data.Dateupdate,
                    Idpemda = data.Idpemda,
                    Idunit = data.Idunit,
                    Idurus = data.Idurus,
                    Kdlevel = data.Kdlevel,
                    Kdunit = data.Kdunit,
                    Nmunit = data.Nmunit,
                    Staktif = data.Staktif,
                    Telepon = data.Telepon,
                    Type = data.Type,
                    IdurusNavigation = urusan_data ?? null,
                    KdlevelNavigation = struunit_data ?? null
                }).AsQueryable();
            if (param.Kdlevel.ToString() != "0")
            {
                query = query.Where(w => w.Kdlevel == param.Kdlevel).AsQueryable();
            }
            Result = await query.ToListAsync();
            return Result;
        }
    }
}
