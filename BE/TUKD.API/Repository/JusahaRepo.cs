using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class JusahaRepo : Repo<Jusaha>, IJusahaRepo
    {
        public JusahaRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Jusaha param)
        {
            Jusaha data = await _tukdContext.Jusaha.Where(w => w.Idjusaha == param.Idjusaha).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Badanusaha = param.Badanusaha;
                data.Keterangan = param.Keterangan;
                data.Akronim = param.Akronim;
                _tukdContext.Jusaha.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
