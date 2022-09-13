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
    public class SpjsppRepo : Repo<Spjspp>, ISpjsppRepo
    {
        public SpjsppRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<List<SpjsppView>> GetBySpp(long Idspp, string Kdstatus)
        {
            List<SpjsppView> datas = new List<SpjsppView> { };
            List<SpjsppView> gets = await _tukdContext.Spjspp.Where(w => w.Idspp == Idspp)
                .Join(_tukdContext.Spj.Where(w => w.Kdstatus.Trim() == Kdstatus.Trim() && w.Tglvalid != null),
                s1 => s1.Idspj,
                s2 => s2.Idspj,
                (s1, s2) => new SpjsppView
                {
                    Idspj = s1.Idspj,
                    Idsppspj = s1.Idsppspj,
                    Idspp = s1.Idspp,
                    Nospj = s2.Nospj,
                    Tglspj = s2.Tglspj,
                    Tglbuku = s2.Tglbuku,
                    Keterangan = s2.Keterangan,
                    Nilai = _tukdContext.Bpkspj.Where(w => w.Idspj == s1.Idspj)
                    .Join(_tukdContext.Bpkdetr,
                    bpkspk => bpkspk.Idbpk, bpkdetr => bpkdetr.Idbpk,
                    (bpkspj, bpkdetr) => new { bpkspj, bpkdetr.Nilai}).Select(s => s.Nilai).Sum()
                }).ToListAsync();
            datas.AddRange(gets);
            return datas;
        }
    }
}
