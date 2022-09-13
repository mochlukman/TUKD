using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class PemdaRepo : Repo<Pemda>, IPemdaRepo
    {
        public PemdaRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<long> GetIdunit()
        {
            Pemda data = await _tukdContext.Pemda.Where(w => w.Configid.Trim() == "curskpkd").FirstOrDefaultAsync();
            if (data != null)
                return Int64.Parse(data.Configval);
            return 0;
        }
    }
}
