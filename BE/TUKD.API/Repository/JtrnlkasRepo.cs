using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class JtrnlkasRepo : Repo<Jtrnlkas>, IJtrnlkasRepo
    {
        public JtrnlkasRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Jtrnlkas param)
        {
            Jtrnlkas data = await _tukdContext.Jtrnlkas.Where(w => w.Idnojetra == param.Idnojetra).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Nmjetra = param.Nmjetra;
                data.Kdpers = param.Kdpers;
                _tukdContext.Jtrnlkas.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
