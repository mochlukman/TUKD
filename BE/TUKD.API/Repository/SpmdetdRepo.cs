using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class SpmdetdRepo : Repo<Spmdetd>, ISpmdetdRepo
    {
        public SpmdetdRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Spmdetd param)
        {
            Spmdetd data = await _tukdContext.Spmdetd.Where(w => w.Idspmdetd == param.Idspmdetd).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Nilai = param.Nilai;
                data.Updatedate = param.Updatedate;
                data.Updateby = param.Updateby;
                _tukdContext.Spmdetd.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
        public async Task<Spmdetd> ViewData(long Idspmdetd)
        {
            Spmdetd data = await (
                    from det in _tukdContext.Spmdetd
                    join rekening in _tukdContext.Daftrekening on det.Idrek equals rekening.Idrek
                    where det.Idspmdetd == Idspmdetd
                    select new Spmdetd
                    {
                        Idspmdetd = det.Idspmdetd,
                        Idspm = det.Idspm,
                        Idnojetra = det.Idnojetra,
                        Idrek = det.Idrek,
                        IdrekNavigation = rekening ?? null,
                        Nilai = det.Nilai
                    }
                ).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<Spmdetd>> ViewDatas(long Idspm)
        {
            List<Spmdetd> datas = await (
                    from det in _tukdContext.Spmdetd
                    join rekening in _tukdContext.Daftrekening on det.Idrek equals rekening.Idrek
                    where det.Idspm == Idspm
                    select new Spmdetd
                    {
                        Idspmdetd = det.Idspmdetd,
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
