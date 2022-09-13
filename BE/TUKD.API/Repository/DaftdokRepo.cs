using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class DaftdokRepo : Repo<Daftdok>, IDaftdokRepo
    {
        public DaftdokRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Daftdok param)
        {
            Daftdok data = await _tukdContext.Daftdok.Where(w => w.Iddaftdok == param.Iddaftdok).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Kddok = param.Kddok;
                data.Nmdok = param.Nmdok;
                data.Ket = param.Kddok;
                data.Dateupdate = param.Dateupdate;
                _tukdContext.Daftdok.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
