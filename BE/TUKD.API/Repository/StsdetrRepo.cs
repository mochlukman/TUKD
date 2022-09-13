using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class StsdetrRepo : Repo<Stsdetr>, IStsdetrRepo
    {
        public StsdetrRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
        public async Task<bool> Update(Stsdetr param)
        {
            Stsdetr data = await _tukdContext.Stsdetr.Where(w => w.Idstsdetr == param.Idstsdetr).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Nilai = param.Nilai;
            data.Dateupdate = param.Dateupdate;
            _tukdContext.Stsdetr.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }
        public async Task<Stsdetr> ViewData(long Idstsdetr)
        {
            Stsdetr data = await (
                    from det in _tukdContext.Stsdetr
                    join rekening in _tukdContext.Daftrekening on det.Idrek equals rekening.Idrek
                    join keg in _tukdContext.Mkegiatan on det.Idkeg equals keg.Idkeg
                    where det.Idstsdetr == Idstsdetr
                    select new Stsdetr
                    {
                        Idstsdetr = det.Idstsdetr,
                        IdkegNavigation = keg ?? null,
                        Idkeg = det.Idkeg,
                        Idsts = det.Idsts,
                        Idnojetra = det.Idnojetra,
                        Idrek = det.Idrek,
                        IdrekNavigation = rekening ?? null,
                        Nilai = det.Nilai
                    }
                ).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<Stsdetr>> ViewDatas(long Idsts)
        {
            List<Stsdetr> datas = await (
                    from det in _tukdContext.Stsdetr
                    join rekening in _tukdContext.Daftrekening on det.Idrek equals rekening.Idrek
                    join keg in _tukdContext.Mkegiatan on det.Idkeg equals keg.Idkeg
                    where det.Idsts == Idsts
                    select new Stsdetr
                    {
                        Idstsdetr = det.Idstsdetr,
                        Idkeg = det.Idkeg,
                        IdkegNavigation = keg ?? null,
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
