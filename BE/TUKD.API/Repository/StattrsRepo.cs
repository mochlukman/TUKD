using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class StattrsRepo : Repo<Stattrs>, IStattrsRepo
    {
        public StattrsRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Stattrs param)
        {
            Stattrs data = await _tukdContext.Stattrs.Where(w => w.Kdstatus.Trim() == param.Kdstatus).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Lblstatus = param.Lblstatus;
                data.Uraian = param.Uraian;
                _tukdContext.Stattrs.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
