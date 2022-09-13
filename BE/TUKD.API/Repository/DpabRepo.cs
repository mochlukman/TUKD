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
    public class DpabRepo : Repo<Dpab>, IDpabRepo
    {
        public DpabRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
        public async Task<bool> UpdateNilai(Dpadetb param, decimal? newTotal)
        {
            Dpab data = await _tukdContext.Dpab.Where(w =>
                                 w.Iddpab == param.Iddpab).FirstOrDefaultAsync();
            data.Nilai = newTotal;
            data.Dateupdate = param.Dateupdate;
            _tukdContext.Dpab.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }
        public async Task<decimal?> GetNilai(long Iddpab)
        {
            decimal? Nilai = await _tukdContext.Dpab.Where(w => w.Iddpab == Iddpab)
                    .Select(s => s.Nilai).SumAsync();
            return Nilai;
        }

        public async Task<List<DpabView>> GetByStsdetd(long Idunit, long Idsts)
        {
            List<long> rekInStsdetd = await _tukdContext.Stsdetd.Where(w => w.Idsts == Idsts).Select(s => s.Idrek).Distinct().ToListAsync();
            List<DpabView> datas = await (
                  from dpab in _tukdContext.Dpab
                  join dpa in _tukdContext.Dpa on dpab.Iddpa equals dpa.Iddpa
                  join rekening in _tukdContext.Daftrekening on dpab.Idrek equals rekening.Idrek
                  where dpa.Idunit == Idunit && !rekInStsdetd.Contains(dpab.Idrek) && rekening.Kdper.StartsWith("6.1.")
                  select new DpabView
                  {
                      Iddpa = dpab.Iddpa,
                      Iddpab = dpab.Iddpab,
                      Datecreate = dpab.Datecreate,
                      Dateupdate = dpab.Dateupdate,
                      Idrek = dpab.Idrek,
                      Kdtahap = dpab.Kdtahap,
                      Nilai = dpab.Nilai,
                      Rekening = rekening
                  }
                ).ToListAsync();
            return datas;
        }
    }
}
