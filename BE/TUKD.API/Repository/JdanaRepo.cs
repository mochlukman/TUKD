using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class JdanaRepo : Repo<Jdana>, IJdanaRepo
    {
        public JdanaRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Udpate(Jdana param)
        {
            Jdana data = await _tukdContext.Jdana.Where(w => w.Idjdana == param.Idjdana).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Kddana = param.Kddana;
                data.Nmdana = param.Nmdana;
                data.Ket = param.Ket;
                _tukdContext.Jdana.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
