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
    public class DpablndRepo : Repo<Dpablnd>, IDpablndRepo
    {
        public DpablndRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
        public async Task<decimal?> TotalNilai(long Iddpad)
        {
            decimal? Total = await _tukdContext.Dpablnd.Where(w => w.Iddpad == Iddpad).Select(s => s.Nilai).SumAsync();
            return Total;
        }

        public async Task<bool> Update(DpablndView param)
        {
            Dpablnd data = await _tukdContext.Dpablnd.Where(w => w.Iddpablnd == param.Iddpablnd).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Nilai = param.Nilai;
                data.Dateupdate = param.Dateupdate;
                _tukdContext.Dpablnd.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
