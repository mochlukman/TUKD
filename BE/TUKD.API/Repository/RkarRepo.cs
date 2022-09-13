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
    public class RkarRepo : Repo<Rkar>, IRkarRepo
    {
        public RkarRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
        public void CalculateNilai(long Idrkar)
        {
            decimal? TotalChild = _tukdContext.Rkadetr.Where(w => w.Idrkar == Idrkar && (w.Idrkadetrduk == 0 || w.Idrkadetrduk.ToString() == null)).Sum(s => s.Subtotal);
            Rkar data = _tukdContext.Rkar.Where(w => w.Idrkar == Idrkar).FirstOrDefault();
            if (data != null)
            {
                data.Nilai = TotalChild;
                _tukdContext.Rkar.Update(data);
                _tukdContext.SaveChanges();
            }
        }

        public async Task<PrimengTableResult<RkarView>> Paging(PrimengTableParam<RkaGlobalGet> param)
        {
            PrimengTableResult<RkarView> Result = new PrimengTableResult<RkarView>();
            IQueryable<RkarView> Query = (
                 from data in _tukdContext.Rkar
                 join rek in _tukdContext.Daftrekening on data.Idrek equals rek.Idrek
                 join mkeg in _tukdContext.Mkegiatan on data.Idkeg equals mkeg.Idkeg
                 select new RkarView
                 {
                     Createddate = data.Createddate,
                     Idrkar = data.Idrkar,
                     Idrkarx = data.Idrkarx,
                     Rkarx = !String.IsNullOrEmpty(data.Idrkarx.ToString()) ? _tukdContext.Rkar.Where(w => w.Idrkar == data.Idrkarx).FirstOrDefault() : null,
                     Idrek = data.Idrek,
                     Createdby = data.Createdby,
                     Idunit = data.Idunit,
                     Kdtahap = data.Kdtahap,
                     Nilai = data.Nilai,
                     IdrekNavigation = rek ?? null,
                     Updateby = data.Updateby,
                     Updatetime = data.Updatetime,
                     Idkeg = data.Idkeg,
                     IdkegNavigation = mkeg ?? null
                 }
                 ).AsQueryable();
            if (param.Parameters.Idunit.ToString() != "0")
            {
                Query = Query.Where(w => w.Idunit == param.Parameters.Idunit).AsQueryable();
            }
            if (param.Parameters.Kdtahap != "x")
            {
                Query = Query.Where(w => w.Kdtahap.Trim() == param.Parameters.Kdtahap.Trim()).AsQueryable();
            }
            if(param.Parameters.Idkeg.ToString() != "0")
            {
                Query = Query.Where(w => w.Idkeg == param.Parameters.Idkeg).AsQueryable();
            }
            if (!String.IsNullOrEmpty(param.GlobalFilter))
            {
                Query = Query.Where(w =>
                    EF.Functions.Like(w.IdrekNavigation.Kdper.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.IdrekNavigation.Nmper.Trim(), "%" + param.GlobalFilter + "%")
                ).AsQueryable();
            }
            if (!String.IsNullOrEmpty(param.SortField))
            {
                if (param.SortField == "idrekNavigation.kdper")
                {
                    if (param.SortOrder > 0)
                    {
                        Query = Query.OrderBy(o => o.IdrekNavigation.Kdper).AsQueryable();
                    }
                    else
                    {
                        Query = Query.OrderByDescending(o => o.IdrekNavigation.Kdper).AsQueryable();
                    }
                }
                else if (param.SortField == "idrekNavigation.nmper")
                {
                    if (param.SortOrder > 0)
                    {
                        Query = Query.OrderBy(o => o.IdrekNavigation.Nmper).AsQueryable();
                    }
                    else
                    {
                        Query = Query.OrderByDescending(o => o.IdrekNavigation.Nmper).AsQueryable();
                    }
                }
                else if (param.SortField == "nilai")
                {
                    if (param.SortOrder > 0)
                    {
                        Query = Query.OrderBy(o => o.Nilai).AsQueryable();
                    }
                    else
                    {
                        Query = Query.OrderByDescending(o => o.Nilai).AsQueryable();
                    }
                }
            }
            Result.Data = await Query.Skip(param.Start).Take(param.Rows).OrderBy(o => o.IdrekNavigation.Kdper).ToListAsync();
            Result.Totalrecords = await Query.CountAsync();
            if (Result.Data.Count() > 0)
            {
                Result.OptionalResult = new PrimengTableResultOptional
                {
                    TotalNilai = Query.Sum(s => s.Nilai)
                };
            }
            Result.Isvalid = await _tukdContext.Rkasah.AnyAsync(w => w.Idunit == param.Parameters.Idunit && w.Kdtahap.Trim() == param.Parameters.Kdtahap.Trim()) ? true : false;
            return Result;
        }

        public async Task<decimal?> TotalNilai(long Idunit)
        {
            decimal? Result = await _tukdContext.Rkar.Where(w => w.Idunit == Idunit).SumAsync(s => s.Nilai);
            return Result;
        }
        public async Task<decimal?> TotalNilaiKeg(long Idunit, long Idkeg)
        {
            decimal? Result = await _tukdContext.Rkar.Where(w => w.Idunit == Idunit && w.Idkeg == Idkeg ).SumAsync(s => s.Nilai);
            return Result;
        }

        public async Task<bool> Update(Rkar param)
        {
            Rkar data = await _tukdContext.Rkar.Where(w => w.Idrkar == param.Idrkar).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Nilai = param.Nilai;
            data.Updateby = param.Updateby;
            data.Updatetime = param.Updatetime;
            _tukdContext.Rkar.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<RkarView> ViewData(long Idrkar)
        {
            RkarView Result = await (
                from data in _tukdContext.Rkar
                join rek in _tukdContext.Daftrekening on data.Idrek equals rek.Idrek
                join mkeg in _tukdContext.Mkegiatan on data.Idkeg equals mkeg.Idkeg
                where data.Idrkar == Idrkar
                select new RkarView
                {
                    Createddate = data.Createddate,
                    Idrkar = data.Idrkar,
                    Idrek = data.Idrek,
                    Idrkarx = data.Idrkarx,
                    Rkarx = !String.IsNullOrEmpty(data.Idrkarx.ToString()) ? _tukdContext.Rkar.Where(w => w.Idrkar == data.Idrkarx).FirstOrDefault() : null,
                    Createdby = data.Createdby,
                    Idunit = data.Idunit,
                    Kdtahap = data.Kdtahap,
                    Nilai = data.Nilai,
                    IdrekNavigation = rek ?? null,
                    Updateby = data.Updateby,
                    Updatetime = data.Updatetime,
                    Idkeg = data.Idkeg,
                    IdkegNavigation = mkeg ?? null
                }
                ).FirstOrDefaultAsync();
            return Result;
        }

        public async Task<List<RkarView>> ViewDatas(RkaGlobalGet param)
        {
            List<RkarView> Result = await (
                from data in _tukdContext.Rkar
                join rek in _tukdContext.Daftrekening on data.Idrek equals rek.Idrek
                join mkeg in _tukdContext.Mkegiatan on data.Idkeg equals mkeg.Idkeg
                where data.Idunit == param.Idunit && data.Kdtahap.Trim() == param.Kdtahap.Trim()
                select new RkarView
                {
                    Createddate = data.Createddate,
                    Idrkar = data.Idrkar,
                    Idrek = data.Idrek,
                    Idrkarx = data.Idrkarx,
                    Rkarx = !String.IsNullOrEmpty(data.Idrkarx.ToString()) ? _tukdContext.Rkar.Where(w => w.Idrkar == data.Idrkarx).FirstOrDefault() : null,
                    Createdby = data.Createdby,
                    Idunit = data.Idunit,
                    Kdtahap = data.Kdtahap,
                    Nilai = data.Nilai,
                    IdrekNavigation = rek ?? null,
                    Updateby = data.Updateby,
                    Updatetime = data.Updatetime,
                    Idkeg = data.Idkeg,
                    IdkegNavigation = mkeg ?? null
                }
                ).ToListAsync();
            return Result;
        }
    }
}
