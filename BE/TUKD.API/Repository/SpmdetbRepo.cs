using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class SpmdetbRepo : Repo<Spmdetb>, ISpmdetbRepo
    {
        public SpmdetbRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
        public async Task<bool> Update(Spmdetb param)
        {
            Spmdetb data = await _tukdContext.Spmdetb.Where(w => w.Idspmdetb == param.Idspmdetb).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Nilai = param.Nilai;
                data.Updatedate = param.Updatedate;
                data.Updateby = param.Updateby;
                _tukdContext.Spmdetb.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
        public async Task<Spmdetb> ViewData(long Idspmdetb)
        {
            Spmdetb data = await (
                    from det in _tukdContext.Spmdetb
                    join rekening in _tukdContext.Daftrekening on det.Idrek equals rekening.Idrek
                    where det.Idspmdetb == Idspmdetb
                    select new Spmdetb
                    {
                        Idspmdetb = det.Idspmdetb,
                        Idspm = det.Idspm,
                        Idnojetra = det.Idnojetra,
                        Idrek = det.Idrek,
                        IdrekNavigation = rekening ?? null,
                        Nilai = det.Nilai
                    }
                ).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<Spmdetb>> ViewDatas(long Idspm)
        {
            List<Spmdetb> datas = await (
                    from det in _tukdContext.Spmdetb
                    join rekening in _tukdContext.Daftrekening on det.Idrek equals rekening.Idrek
                    where det.Idspm == Idspm
                    select new Spmdetb
                    {
                        Idspmdetb = det.Idspmdetb,
                        Idspm = det.Idspm,
                        Idnojetra = det.Idnojetra,
                        Idrek = det.Idrek,
                        IdrekNavigation = rekening ?? null,
                        Nilai = det.Nilai
                    }
                ).ToListAsync();
            return datas;
        }
    }
}
