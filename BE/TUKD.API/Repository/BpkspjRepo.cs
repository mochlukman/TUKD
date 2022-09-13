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
    public class BpkspjRepo : Repo<Bpkspj>, IBpkspjRepo
    {
        public BpkspjRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<List<BpkspjView>> ByBpk(long Idbpk)
        {
            List<BpkspjView> data = await (
                from bpkspj in _tukdContext.Bpkspj
                join bpk in _tukdContext.Bpk on bpkspj.Idbpk equals bpk.Idbpk
                where bpkspj.Idbpk == Idbpk
                select new BpkspjView
                {
                    Datecreate = bpkspj.Datecreate,
                    Dateupdate = bpkspj.Dateupdate,
                    Idbpk = bpkspj.Idbpk,
                    Idspj = bpkspj.Idspj,
                    Idbpkspj = bpkspj.Idbpkspj,
                    IdbpkNavigation = bpk ?? null,
                    Nilai = _tukdContext.Bpkdetr.Where(w => w.Idbpk == bpkspj.Idbpk).Select(s => s.Nilai).Sum()
                }
                ).ToListAsync();
            return data;
        }

        public async Task<List<BpkspjView>> BySpj(long Idspj)
        {
            List<BpkspjView> data = await (
                from bpkspj in _tukdContext.Bpkspj
                join bpk in _tukdContext.Bpk on bpkspj.Idbpk equals bpk.Idbpk
                where bpkspj.Idspj == Idspj
                select new BpkspjView
                {
                    Datecreate = bpkspj.Datecreate,
                    Dateupdate = bpkspj.Dateupdate,
                    Idbpk = bpkspj.Idbpk,
                    Idspj = bpkspj.Idspj,
                    Idbpkspj = bpkspj.Idbpkspj,
                    IdbpkNavigation = bpk ?? null,
                    Nilai = _tukdContext.Bpkdetr.Where(w => w.Idbpk == bpkspj.Idbpk).Select(s => s.Nilai).Sum()
                }
                ).ToListAsync();
            return data;
        }

        public async Task<BpkspjView> ViewData(long Idbpkspj)
        {
            BpkspjView data = await (
                from bpkspj in _tukdContext.Bpkspj
                join bpk in _tukdContext.Bpk on bpkspj.Idbpk equals bpk.Idbpk
                where bpkspj.Idbpkspj == Idbpkspj
                select new BpkspjView
                {
                    Datecreate = bpkspj.Datecreate,
                    Dateupdate = bpkspj.Dateupdate,
                    Idbpk = bpkspj.Idbpk,
                    Idspj = bpkspj.Idspj,
                    Idbpkspj = bpkspj.Idbpkspj,
                    IdbpkNavigation = bpk ?? null,
                    Nilai = _tukdContext.Bpkdetr.Where(w => w.Idbpk == bpkspj.Idbpk).Select(s => s.Nilai).Sum()
                }
                ).FirstOrDefaultAsync();
            return data;
        }
    }
}
