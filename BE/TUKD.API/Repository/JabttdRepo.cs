using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class JabttdRepo : Repo<Jabttd>, IJabttdRepo
    {
        public JabttdRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Jabttd param)
        {
            Jabttd data = await _tukdContext.Jabttd.Where(w => w.Idttd == param.Idttd).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Idunit = param.Idunit;
                data.Idpeg = param.Idpeg;
                data.Kddok = param.Kddok;
                data.Jabatan = param.Jabatan;
                data.Dateupdate = param.Dateupdate;
                _tukdContext.Jabttd.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
