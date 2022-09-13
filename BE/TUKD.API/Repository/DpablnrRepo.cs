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
    public class DpablnrRepo : Repo<Dpablnr>, IDpablnrRepo
    {
        public DpablnrRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
        public async Task<decimal?> TotalNilai(long Iddpar)
        {
            decimal? Total = await _tukdContext.Dpablnr.Where(w => w.Iddpar == Iddpar).Select(s => s.Nilai).SumAsync();
            return Total;
        }

        public async Task<bool> Update(DpablnrView param)
        {
            Dpablnr data = await _tukdContext.Dpablnr.Where(w => w.Iddpablnr == param.Iddpablnr).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Nilai = param.Nilai;
                data.Dateupdate = param.Dateupdate;
                _tukdContext.Dpablnr.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
