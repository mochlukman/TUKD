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
    public class RkatapddRepo : Repo<Rkatapdd>, IRkatapddRepo
    {
        public RkatapddRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<string> GenerateNomor(long Idrka)
        {
            string newno = "";
            string lastno = await _tukdContext.Rkatapdd.Where(w => w.Idrkad == Idrka).OrderBy(o => o.Nomor.Trim()).Select(s => s.Nomor).LastOrDefaultAsync();
            if (string.IsNullOrEmpty(lastno))
            {
                newno = "01";
            }
            else
            {
                var toNumber = Int32.Parse(lastno);
                var PlusNumber = toNumber + 1;
                if (PlusNumber.ToString().Length == 1) newno = "0" + PlusNumber.ToString();
                if (PlusNumber.ToString().Length == 2) newno = PlusNumber.ToString();
            }
            return newno;
        }

        public async Task<PrimengTableResult<RkatapddView>> Paging(PrimengTableParam<RkatapdGet> param)
        {
            PrimengTableResult<RkatapddView> Result = new PrimengTableResult<RkatapddView>();
            IQueryable<RkatapddView> query = (
                from data in _tukdContext.Rkatapdd
                join rka in _tukdContext.Rkad on data.Idrkad equals rka.Idrkad
                join pegawai in _tukdContext.Pegawai on data.Idpeg equals pegawai.Idpeg
                select new RkatapddView
                {
                    Createdby = data.Createdby,
                    Createddate = data.Createddate,
                    Idpeg = data.Idpeg,
                    Idrkad = data.Idrkad,
                    Idtapdd = data.Idtapdd,
                    Keterangan = data.Keterangan,
                    Nomor = data.Nomor,
                    Tanggal = data.Tanggal,
                    Updateby = data.Updateby,
                    Verifikasi = data.Verifikasi,
                    Updatetime = data.Updatetime,
                    IdpegNavigation = pegawai ?? null,
                    Nippeg = pegawai != null ? pegawai.Nip : "",
                    Namapeg = pegawai != null ? pegawai.Nama : "",
                    IdrkadNavigation = rka ?? null
                }
                ).AsQueryable();
            if(param.Parameters.Idrka.ToString() != "0")
            {
                query = query.Where(w => w.Idrkad == param.Parameters.Idrka).AsQueryable();
            }
            if(param.Parameters.Idpeg.ToString() != "0")
            {
                query = query.Where(w => w.Idpeg == param.Parameters.Idpeg).AsQueryable();
            }
            if (!String.IsNullOrEmpty(param.GlobalFilter))
            {
                query = query.Where(w =>
                    EF.Functions.Like(w.Nomor.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Keterangan.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Nippeg.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Namapeg.Trim(), "%" + param.GlobalFilter + "%") ||
                    EF.Functions.Like(w.Verifikasi.Trim(), "%" + param.GlobalFilter + "%")
                ).AsQueryable();
            }
            if (!String.IsNullOrEmpty(param.SortField))
            {
                if (param.SortField == "nomor")
                {
                    if (param.SortOrder > 0)
                    {
                        query = query.OrderBy(o => o.Nomor).AsQueryable();
                    }
                    else
                    {
                        query = query.OrderByDescending(o => o.Nomor).AsQueryable();
                    }
                }
                else if (param.SortField == "nippeg")
                {
                    if (param.SortOrder > 0)
                    {
                        query = query.OrderBy(o => o.Nippeg).AsQueryable();
                    }
                    else
                    {
                        query = query.OrderByDescending(o => o.Nippeg).AsQueryable();
                    }
                }
                else if (param.SortField == "verifikasi")
                {
                    if (param.SortOrder > 0)
                    {
                        query = query.OrderBy(o => o.Verifikasi).AsQueryable();
                    }
                    else
                    {
                        query = query.OrderByDescending(o => o.Verifikasi).AsQueryable();
                    }
                }
                else if (param.SortField == "tanggal")
                {
                    if (param.SortOrder > 0)
                    {
                        query = query.OrderBy(o => o.Tanggal).AsQueryable();
                    }
                    else
                    {
                        query = query.OrderByDescending(o => o.Tanggal).AsQueryable();
                    }
                }
            }
            Result.Data = await query.Skip(param.Start).Take(param.Rows).ToListAsync();
            Result.Totalrecords = await query.CountAsync();
            return Result;
        }

        public async Task<bool> Update(Rkatapdd param)
        {
            Rkatapdd data = await _tukdContext.Rkatapdd.Where(w => w.Idtapdd == param.Idtapdd).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Updateby = param.Updateby;
            data.Updatetime = param.Updatetime;
            data.Nomor = param.Nomor;
            data.Verifikasi = param.Verifikasi;
            data.Keterangan = param.Keterangan;
            data.Tanggal = param.Tanggal;
            _tukdContext.Rkatapdd.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<RkatapddView> ViewData(long Idtapd)
        {
            RkatapddView Result = await (
                from data in _tukdContext.Rkatapdd
                join rka in _tukdContext.Rkad on data.Idrkad equals rka.Idrkad
                join pegawai in _tukdContext.Pegawai on data.Idpeg equals pegawai.Idpeg
                where data.Idtapdd == Idtapd
                select new RkatapddView
                {
                    Createdby = data.Createdby,
                    Createddate = data.Createddate,
                    Idpeg = data.Idpeg,
                    Idrkad = data.Idrkad,
                    Idtapdd = data.Idtapdd,
                    Keterangan = data.Keterangan,
                    Nomor = data.Nomor,
                    Tanggal = data.Tanggal,
                    Updateby = data.Updateby,
                    Verifikasi = data.Verifikasi,
                    Updatetime = data.Updatetime,
                    IdpegNavigation = pegawai ?? null,
                    Nippeg = pegawai != null ? pegawai.Nip : "",
                    Namapeg = pegawai != null ? pegawai.Nama : "",
                    IdrkadNavigation = rka ?? null
                }
                ).FirstOrDefaultAsync();
            return Result;
        }
    }
}
