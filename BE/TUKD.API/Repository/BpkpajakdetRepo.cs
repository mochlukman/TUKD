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
    public class BpkpajakdetRepo : Repo<Bpkpajakdet>, IBpkpajakdetRepo
    {
        public BpkpajakdetRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<decimal?> sumNilai(long Idbpkpajak)
        {
            decimal? total = await _tukdContext.Bpkpajakdet.Where(w => w.Idbpkpajak == Idbpkpajak).SumAsync(s => s.Nilai);
            return total;
        }

        public async Task<bool> Update(Bpkpajakdet param)
        {
            Bpkpajakdet data = await _tukdContext.Bpkpajakdet.Where(w => w.Idbpkpajakdet == param.Idbpkpajakdet).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Nilai = param.Nilai;
            data.Dateupdate = param.Dateupdate;
            _tukdContext.Bpkpajakdet.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }

        public async Task<Bpkpajakdet> ViewData(long Idbpkpajakdet)
        {
            Bpkpajakdet Result = await (
                from data in _tukdContext.Bpkpajakdet
                join bpkpajak in _tukdContext.Bpkpajak on data.Idbpkpajak equals bpkpajak.Idbpkpajak
                join pajak in _tukdContext.Pajak on data.Idpajak equals pajak.Idpajak
                where data.Idbpkpajakdet == Idbpkpajakdet
                select new Bpkpajakdet
                {
                    Datecreate = data.Datecreate,
                    Dateupdate = data.Dateupdate,
                    Idpajak = data.Idpajak,
                    Idbpkpajak = data.Idbpkpajak,
                    Nilai = data.Nilai,
                    Idbpkpajakdet = data.Idbpkpajakdet,
                    IdbpkpajakNavigation = bpkpajak ?? null,
                    IdpajakNavigation = pajak ?? null
                }
                ).FirstOrDefaultAsync();
            return Result;
        }

        public async Task<List<Bpkpajakdet>> ViewDatas(BpkpajakdetGet param)
        {
            List<Bpkpajakdet> Result = new List<Bpkpajakdet>();
            IQueryable<Bpkpajakdet> query = (
                from data in _tukdContext.Bpkpajakdet
                join bpkpajak in _tukdContext.Bpkpajak on data.Idbpkpajak equals bpkpajak.Idbpkpajak
                join pajak in _tukdContext.Pajak on data.Idpajak equals pajak.Idpajak
                select new Bpkpajakdet
                {
                    Datecreate = data.Datecreate,
                    Dateupdate = data.Dateupdate,
                    Idpajak = data.Idpajak,
                    Idbpkpajak = data.Idbpkpajak,
                    Nilai = data.Nilai,
                    Idbpkpajakdet = data.Idbpkpajakdet,
                    IdbpkpajakNavigation = bpkpajak ?? null,
                    IdpajakNavigation = pajak ?? null
                }
                ).AsQueryable();
            if(param.Idbpkpajak.ToString() != "0")
            {
                query = query.Where(w => w.Idbpkpajak == param.Idbpkpajak).AsQueryable();
            }
            Result = await query.ToListAsync();
            return Result;
        }
    }
}
