using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class SpddetbRepo : Repo<Spddetb>, ISpddetbRepo
    {
        public SpddetbRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
        public async Task<List<long>> GetIdReks(long Idspd, long Idkeg)
        {
            List<long> IdReks = new List<long> { };
            if (Idkeg != 0)
            {
                IdReks.AddRange(await _tukdContext.Spddetb.Where(w => w.Idspd == Idspd).Select(s => s.Idrek).Distinct().ToListAsync());
            }
            else
            {
                IdReks.AddRange(await _tukdContext.Spddetb.Where(w => w.Idspd == Idspd).Select(s => s.Idrek).Distinct().ToListAsync());
            }
            return IdReks;
        }

        public async Task<decimal?> TotalNilaiSpd(long Idspd)
        {
            decimal? Total = await _tukdContext.Spddetb.Where(w => w.Idspd == Idspd).SumAsync(s => s.Nilai);
            return Total;
        }
    }
}
