using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class DpdetRepo : Repo<Dpdet>, IDpdetRepo
    {
        public DpdetRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<Dpdet> ViewData(long Iddpdet)
        {
            Dpdet data = await (
                from det in _tukdContext.Dpdet
                join sp2d in _tukdContext.Sp2d on det.Idsp2d equals sp2d.Idsp2d
                where det.Iddpdet == Iddpdet
                select new Dpdet
                {
                    Iddp = det.Iddp,
                    Iddpdet = det.Iddpdet,
                    Idsp2d = det.Idsp2d,
                    Datecreate = det.Datecreate,
                    Idsp2dNavigation = sp2d ?? null
                }
                ).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<Dpdet>> ViewDatas(long Iddp)
        {
            List<Dpdet> data = await (
                from det in _tukdContext.Dpdet
                join sp2d in _tukdContext.Sp2d on det.Idsp2d equals sp2d.Idsp2d
                where det.Iddp == Iddp
                select new Dpdet
                {
                    Iddp = det.Iddp,
                    Iddpdet = det.Iddpdet,
                    Idsp2d = det.Idsp2d,
                    Datecreate = det.Datecreate,
                    Idsp2dNavigation = sp2d ?? null
                }
                ).ToListAsync();
            return data;
        }
    }
}
