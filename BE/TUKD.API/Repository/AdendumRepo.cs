using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class AdendumRepo : Repo<Adendum>, IAdendumRepo
    {
        public AdendumRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Adendum param)
        {
            Adendum data = await _tukdContext.Adendum.Where(w => w.Idadd == param.Idadd).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Noadd = param.Noadd;
                data.Tgladd = param.Tgladd;
                data.Idkontrak = param.Idkontrak;
                data.Nilaiadd = param.Nilaiadd;
                data.Nilaiawal = param.Nilaiawal;
                data.Dateupdate = param.Dateupdate;
                _tukdContext.Adendum.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
