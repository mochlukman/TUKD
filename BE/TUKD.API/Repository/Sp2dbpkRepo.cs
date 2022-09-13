using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class Sp2dbpkRepo : Repo<Sp2dbpk>, ISp2dbpkRepo
    {
        public Sp2dbpkRepo(DbContext context) : base(context)
        {
        }
        TukdContext _tukdContext => _context as TukdContext;

        public async Task<List<long>> Sp2dIds()
        {
            List<long> datas = await _tukdContext.Sp2dbpk.Select(s => s.Idsp2d).ToListAsync();
            return datas;
        }
    }
}
