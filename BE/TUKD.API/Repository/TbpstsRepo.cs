using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class TbpstsRepo : Repo<Tbpsts>, ITbpstsRepo
    {
        public TbpstsRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<List<Tbpsts>> GetBySts(long Idsts)
        {
            List<Tbpsts> datas = await (
                from data in _tukdContext.Tbpsts
                join tbp in _tukdContext.Tbp on data.Idtbp equals tbp.Idtbp
                join sts in _tukdContext.Sts on data.Idsts equals sts.Idsts
                where data.Idsts == Idsts
                select new Tbpsts
                {
                    Idsts = data.Idsts,
                    Idtbp = data.Idtbp,
                    IdstsNavigation = sts ?? null,
                    IdtbpNavigation = tbp ?? null
                }
                ).ToListAsync();
            return datas;
        }

        public async Task<List<Tbpsts>> GetByTbp(long Idtbp)
        {
            List<Tbpsts> datas = await(
                from data in _tukdContext.Tbpsts
                join tbp in _tukdContext.Tbp on data.Idtbp equals tbp.Idtbp
                join sts in _tukdContext.Sts on data.Idsts equals sts.Idsts
                where data.Idtbp == Idtbp
                select new Tbpsts
                {
                    Idsts = data.Idsts,
                    Idtbp = data.Idtbp,
                    IdstsNavigation = sts ?? null,
                    IdtbpNavigation = tbp ?? null
                }
                ).ToListAsync();
            return datas;
        }

        public async Task<Tbpsts> ViewData(long Idtbp, long Idsts)
        {
            Tbpsts datas = await (
                from data in _tukdContext.Tbpsts
                join tbp in _tukdContext.Tbp on data.Idtbp equals tbp.Idtbp
                join sts in _tukdContext.Sts on data.Idsts equals sts.Idsts
                where data.Idtbp == Idtbp && data.Idsts == Idsts
                select new Tbpsts
                {
                    Idsts = data.Idsts,
                    Idtbp = data.Idtbp,
                    IdstsNavigation = sts ?? null,
                    IdtbpNavigation = tbp ?? null
                }
                ).FirstOrDefaultAsync();
            return datas;
        }
    }
}
