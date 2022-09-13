using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Repository
{
    public class BkbankRepo : Repo<Bkbank>, IBkbankRepo
    {
        public BkbankRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Bkbank param)
        {
            Bkbank data = await _tukdContext.Bkbank.Where(w => w.Idbkbank == param.Idbkbank).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Nobuku = param.Nobuku;
            data.Uraian = param.Uraian;
            data.Tglbuku = param.Tglbuku;
            data.Tglvalid = param.Tglvalid;
            _tukdContext.Bkbank.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0) return true;
            return false;
        }
        public async Task<List<long>> GetIds(long Idunit, string Kdstatus, long Idbend)
        {
            List<long> Ids = await _tukdContext.Bkbank
                .Where(w => w.Idunit == Idunit && w.Kdstatus.Trim() == Kdstatus.Trim() && w.Idbend == Idbend)
                .Select(s => s.Idbkbank).ToListAsync();
            return Ids;
        }
        public async Task<Bkbank> ViewData(long Idbkbank)
        {
            Bkbank Result = await (
                from data in _tukdContext.Bkbank
                join unit in _tukdContext.Daftunit on data.Idunit equals unit.Idunit
                join bend in _tukdContext.Bend on data.Idbend equals bend.Idbend
                join status in _tukdContext.Stattrs on data.Kdstatus.Trim() equals status.Kdstatus.Trim()
                where data.Idbkbank == Idbkbank
                select new Bkbank
                {
                    Idbend = data.Idbend,
                    Kdstatus = data.Kdstatus,
                    KdstatusNavigation = status ?? null,
                    Idunit = data.Idunit,
                    Nobuku = data.Nobuku,
                    Idbkbank = data.Idbkbank,
                    Tglbuku = data.Tglbuku,
                    Tglvalid = data.Tglvalid,
                    Uraian = data.Uraian,
                    IdunitNavigation = unit ?? null,
                    IdbendNavigation = bend ?? null
                }
                ).FirstOrDefaultAsync();
            return Result;
        }

        public async Task<List<Bkbank>> ViewDatas(BkbankGet param)
        {
            List<Bkbank> Result = new List<Bkbank>();
            IQueryable<Bkbank> query = (
                from data in _tukdContext.Bkbank
                join unit in _tukdContext.Daftunit on data.Idunit equals unit.Idunit
                join bend in _tukdContext.Bend on data.Idbend equals bend.Idbend
                join status in _tukdContext.Stattrs on data.Kdstatus.Trim() equals status.Kdstatus.Trim()
                select new Bkbank
                {
                    Idbend = data.Idbend,
                    Kdstatus = data.Kdstatus,
                    KdstatusNavigation = status ?? null,
                    Idunit = data.Idunit,
                    Nobuku = data.Nobuku,
                    Idbkbank = data.Idbkbank,
                    Tglbuku = data.Tglbuku,
                    Tglvalid = data.Tglvalid,
                    Uraian = data.Uraian,
                    IdunitNavigation = unit ?? null,
                    IdbendNavigation = bend ?? null
                }
                ).AsQueryable();
            if(param.Idunit.ToString() != "0")
            {
                query = query.Where(w => w.Idunit == param.Idunit).AsQueryable();
            }
            if(param.Idbend.ToString() != "0")
            {
                query = query.Where(w => w.Idbend == param.Idbend).AsQueryable();
            }
            Result = await query.ToListAsync();
            return Result;
        }
    }
}
