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
    public class BpkpajakstrdetRepo : Repo<Bpkpajakstrdet>, IBpkpajakstrdetRepo
    {
        public BpkpajakstrdetRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
        public async Task<bool> Update(Bpkpajakstrdet param)
        {
            Bpkpajakstrdet data = await _tukdContext.Bpkpajakstrdet.Where(w => w.Idbpkpajakstrdet == param.Idbpkpajakstrdet).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Nilai = param.Nilai;
            data.Dateupdate = param.Dateupdate;
            _tukdContext.Bpkpajakstrdet.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<Bpkpajakstrdet> ViewData(long Idbpkpajakstrdet)
        {
            Bpkpajakstrdet Result = await (
                from data in _tukdContext.Bpkpajakstrdet
                join bpkpajakstr in _tukdContext.Bpkpajakstr on data.Idbpkpajakstr equals bpkpajakstr.Idbpkpajakstr
                join bpkpajak in _tukdContext.Bpkpajak on data.Idbpkpajak equals bpkpajak.Idbpkpajak into bpkpajakMatch from bpkpajak_data in bpkpajakMatch
                where data.Idbpkpajakstrdet == Idbpkpajakstrdet
                select new Bpkpajakstrdet
                {
                    Datecreate = data.Datecreate,
                    Dateupdate = data.Dateupdate,
                    Idbpkpajak = data.Idbpkpajak,
                    Idbpkpajakstr = data.Idbpkpajakstr,
                    Nilai = data.Nilai,
                    Idbpkpajakstrdet = data.Idbpkpajakstrdet,
                    IdbpkpajakstrNavigation = bpkpajakstr ?? null,
                    IdbpkpajakNavigation = bpkpajak_data ?? null
                }
                ).FirstOrDefaultAsync();
            return Result;
        }

        public async Task<List<Bpkpajakstrdet>> ViewDatas(BpkpajakstrdetGet param)
        {
            List<Bpkpajakstrdet> Result = new List<Bpkpajakstrdet>();
            IQueryable<Bpkpajakstrdet> query = (
                from data in _tukdContext.Bpkpajakstrdet
                join bpkpajakstr in _tukdContext.Bpkpajakstr on data.Idbpkpajakstr equals bpkpajakstr.Idbpkpajakstr
                join bpkpajak in _tukdContext.Bpkpajak on data.Idbpkpajak equals bpkpajak.Idbpkpajak into bpkpajakMatch from bpkpajak_data in bpkpajakMatch
                select new Bpkpajakstrdet
                {
                    Datecreate = data.Datecreate,
                    Dateupdate = data.Dateupdate,
                    Idbpkpajak = data.Idbpkpajak,
                    Idbpkpajakstr = data.Idbpkpajakstr,
                    Nilai = data.Nilai,
                    Idbpkpajakstrdet = data.Idbpkpajakstrdet,
                    IdbpkpajakstrNavigation = bpkpajakstr ?? null,
                    IdbpkpajakNavigation = bpkpajak_data ?? null
                }
                ).AsQueryable();
            if (param.Idbpkpajakstr.ToString() != "0")
            {
                query = query.Where(w => w.Idbpkpajakstr == param.Idbpkpajakstr).AsQueryable();
            }
            Result = await query.ToListAsync();
            return Result;
        }
    }
}
