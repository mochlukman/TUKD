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
    public class BpkpajakstrRepo : Repo<Bpkpajakstr>, IBpkpajakstrRepo
    {
        public BpkpajakstrRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
        public async Task<bool> Update(Bpkpajakstr param)
        {
            Bpkpajakstr data = await _tukdContext.Bpkpajakstr.Where(w => w.Idbpkpajakstr == param.Idbpkpajakstr).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Dateupdate = param.Dateupdate;
            data.Tanggal = param.Tanggal;
            data.Nomor = param.Nomor;
            data.Uraian = param.Uraian;
            _tukdContext.Bpkpajakstr.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0) return true;
            return false;
        }

        public async Task<BpkpajakstrView> ViewData(long Idbpkpajakstr)
        {
            BpkpajakstrView Result = await (
                from data in _tukdContext.Bpkpajakstr
                join unit in _tukdContext.Daftunit on data.Idunit equals unit.Idunit
                join status in _tukdContext.Stattrs on data.Kdstatus.Trim() equals status.Kdstatus.Trim() into statusMatch
                from status_data in statusMatch.DefaultIfEmpty()
                where data.Idbpkpajakstr == Idbpkpajakstr
                select new BpkpajakstrView
                {
                    Datecreate = data.Datecreate,
                    Dateupdate = data.Dateupdate,
                    Idunit = data.Idunit,
                    Uraian = data.Uraian,
                    Kdstatus = data.Kdstatus,
                    Tanggal = data.Tanggal,
                    Nomor = data.Nomor,
                    Idbpkpajakstr = data.Idbpkpajakstr,
                    IdunitNavigation = unit ?? null,
                    KdstatusNavigation = status_data ?? null,
                    Nilai = _tukdContext.Bpkpajakstrdet.Where(w => w.Idbpkpajakstr == data.Idbpkpajakstr).Sum(s => s.Nilai)
                }
                ).FirstOrDefaultAsync();
            return Result;
        }

        public async Task<List<BpkpajakstrView>> ViewDatas(BpkpajakstrGet param)
        {
            List<BpkpajakstrView> Result = new List<BpkpajakstrView>();
            IQueryable<BpkpajakstrView> query = (
                from data in _tukdContext.Bpkpajakstr
                join unit in _tukdContext.Daftunit on data.Idunit equals unit.Idunit
                join status in _tukdContext.Stattrs on data.Kdstatus.Trim() equals status.Kdstatus.Trim() into statusMatch
                from status_data in statusMatch.DefaultIfEmpty()
                select new BpkpajakstrView
                {
                    Datecreate = data.Datecreate,
                    Dateupdate = data.Dateupdate,
                    Idunit = data.Idunit,
                    Uraian = data.Uraian,
                    Kdstatus = data.Kdstatus,
                    Tanggal = data.Tanggal,
                    Nomor = data.Nomor,
                    Idbpkpajakstr = data.Idbpkpajakstr,
                    IdunitNavigation = unit ?? null,
                    KdstatusNavigation = status_data ?? null,
                    Nilai = _tukdContext.Bpkpajakstrdet.Where(w => w.Idbpkpajakstr == data.Idbpkpajakstr).Sum(s => s.Nilai)
                }
                ).AsQueryable();
            if (param.Idunit.ToString() != "0")
            {
                query = query.Where(w => w.Idunit == param.Idunit).AsQueryable();
            }
            if (param.Kdstatus.Trim() != "x")
            {
                List<string> status = param.Kdstatus.Split(",").ToList();
                query = query.Where(w => status.Contains(w.Kdstatus.Trim())).AsQueryable();
            }
            Result = await query.ToListAsync();
            return Result;
        }
    }
}
