using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class SppdetbRepo : Repo<Sppdetb>, ISppdetbRepo
    {
        public SppdetbRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
        public async Task<decimal?> TotalNilaiSpp(List<long> Idspp)
        {
            decimal? total = await _tukdContext.Sppdetb.Where(w => Idspp.Contains(w.Idspp)).SumAsync(s => s.Nilai);
            return total;
        }
        public async Task<bool> Update(Sppdetb param)
        {
            Sppdetb data = await _tukdContext.Sppdetb.Where(w => w.Idsppdetb == param.Idsppdetb).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Nilai = param.Nilai;
                data.Updatedate = param.Updatedate;
                data.Updateby = param.Updateby;
                _tukdContext.Sppdetb.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
        public async Task<bool> UpdateNilai(long Idsspdetb, decimal? Nilai, DateTime? Dateupdate, string Updateby)
        {
            Sppdetb data = await _tukdContext.Sppdetb.Where(w => w.Idsppdetb == Idsspdetb).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Nilai = Nilai;
                data.Updatedate = Dateupdate;
                data.Updateby = Updateby;
                _tukdContext.Sppdetb.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
