using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class JkegRepo : Repo<Jkeg>, IJkegRepo
    {
        public JkegRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Jkeg param)
        {
            Jkeg data = await _tukdContext.Jkeg.Where(w => w.Jnskeg == param.Jnskeg).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Uraian = param.Uraian;
                _tukdContext.Jkeg.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
