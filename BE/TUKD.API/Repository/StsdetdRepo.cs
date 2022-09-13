using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class StsdetdRepo : Repo<Stsdetd>, IStsdetdRepo
    {
        public StsdetdRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Stsdetd param)
        {
            Stsdetd data = await _tukdContext.Stsdetd.Where(w => w.Idstsdetd == param.Idstsdetd).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Nilai = param.Nilai;
            data.Dateupdate = param.Dateupdate;
            _tukdContext.Stsdetd.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }
        public async Task<Stsdetd> ViewData(long Idstsdetd)
        {
            Stsdetd data = await (
                    from det in _tukdContext.Stsdetd
                    join rekening in _tukdContext.Daftrekening on det.Idrek equals rekening.Idrek
                    where det.Idstsdetd == Idstsdetd
                    select new Stsdetd
                    {
                        Idstsdetd = det.Idstsdetd,
                        Idsts = det.Idsts,
                        Idnojetra = det.Idnojetra,
                        Idrek = det.Idrek,
                        IdrekNavigation = rekening ?? null,
                        Nilai = det.Nilai
                    }
                ).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<Stsdetd>> ViewDatas(long Idsts)
        {
            List<Stsdetd> datas = await (
                    from det in _tukdContext.Stsdetd
                    join rekening in _tukdContext.Daftrekening on det.Idrek equals rekening.Idrek
                    where det.Idsts == Idsts
                    select new Stsdetd
                    {
                        Idstsdetd = det.Idstsdetd,
                        Idsts = det.Idsts,
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
