using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class JnspajakRepo : Repo<Jnspajak>, IJnspajakRepo
    {
        public JnspajakRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
        public async Task<bool> Update(Jnspajak param)
        {
            Jnspajak data = await _tukdContext.Jnspajak.Where(w => w.Idjnspajak == param.Idjnspajak).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Nmjnspajak = param.Nmjnspajak;
                _tukdContext.Jnspajak.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
