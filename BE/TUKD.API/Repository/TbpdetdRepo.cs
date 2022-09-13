using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class TbpdetdRepo : Repo<Tbpdetd>, ITbpdetdRepo
    {
        public TbpdetdRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Tbpdetd param)
        {
            Tbpdetd data = await _tukdContext.Tbpdetd.Where(w => w.Idtbpdetd == param.Idtbpdetd).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Nilai = param.Nilai;
            _tukdContext.Tbpdetd.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }
        public async Task<Tbpdetd> ViewData(long Idtbpdetd)
        {
            Tbpdetd data = await (
                    from det in _tukdContext.Tbpdetd
                    join rekening in _tukdContext.Daftrekening on det.Idrek equals rekening.Idrek
                    where det.Idtbpdetd == Idtbpdetd
                    select new Tbpdetd
                    {
                        Idtbpdetd = det.Idtbpdetd,
                        Idtbp = det.Idtbp,
                        Idnojetra = det.Idnojetra,
                        Idrek = det.Idrek,
                        IdrekNavigation = rekening ?? null,
                        Nilai = det.Nilai
                    }
                ).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<Tbpdetd>> ViewDatas(long Idtbp)
        {
            List<Tbpdetd> datas = await (
                    from det in _tukdContext.Tbpdetd
                    join rekening in _tukdContext.Daftrekening on det.Idrek equals rekening.Idrek
                    where det.Idtbp == Idtbp
                    select new Tbpdetd
                    {
                        Idtbpdetd = det.Idtbpdetd,
                        Idtbp = det.Idtbp,
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
