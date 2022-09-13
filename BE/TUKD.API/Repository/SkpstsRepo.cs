using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class SkpstsRepo : Repo<Skpsts>, ISkpstsRepo
    {
        public SkpstsRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<Skpsts> ViewData(long Idskp, long Idsts)
        {
            Skpsts datas = await (
                from data in _tukdContext.Skpsts
                join skp in _tukdContext.Skp on data.Idskp equals skp.Idskp
                join sts in _tukdContext.Sts on data.Idsts equals sts.Idsts
                where data.Idskp == Idskp && data.Idsts == Idsts
                select new Skpsts
                {
                    Idskp = data.Idskp,
                    Idsts = data.Idsts,
                    IdskpNavigation = skp ?? null,
                    IdstsNavigation = sts ?? null
                }
                ).FirstOrDefaultAsync();
            return datas;
        }
    }
}
