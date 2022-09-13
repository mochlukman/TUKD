using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class JtermorlunRepo : Repo<Jtermorlun>, IJtermorlunRepo
    {
        public JtermorlunRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Jtermorlun param)
        {
            Jtermorlun data = await _tukdContext.Jtermorlun.Where(w => w.Idjtermorlun == param.Idjtermorlun).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Nmjtermorlun = param.Nmjtermorlun;
                _tukdContext.Jtermorlun.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
