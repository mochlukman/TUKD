using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class JbendRepo : Repo<Jbend>, IJbendRepo
    {
        public JbendRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Jbend param)
        {
            Jbend data = await _tukdContext.Jbend.Where(w => w.Jnsbend.Trim() == param.Jnsbend.Trim()).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Idrek = param.Idrek;
                data.Uraibend = param.Uraibend;
                _tukdContext.Jbend.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
