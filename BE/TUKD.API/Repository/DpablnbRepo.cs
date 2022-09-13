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
    public class DpablnbRepo : Repo<Dpablnb>, IDpablnbRepo
    {
        public DpablnbRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
        public async Task<decimal?> TotalNilai(long Iddpab)
        {
            decimal? Total = await _tukdContext.Dpablnb.Where(w => w.Iddpab == Iddpab).Select(s => s.Nilai).SumAsync();
            return Total;
        }

        public async Task<bool> Update(DpablnbView param)
        {
            Dpablnb data = await _tukdContext.Dpablnb.Where(w => w.Iddpablnb == param.Iddpablnb).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Nilai = param.Nilai;
                data.Dateupdate = param.Dateupdate;
                _tukdContext.Dpablnb.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
