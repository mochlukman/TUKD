using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class KontrakRepo : Repo<Kontrak>, IKontrakRepo
    {
        public KontrakRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Kontrak param)
        {
            Kontrak data = await _tukdContext.Kontrak.Where(w => w.Idkontrak == param.Idkontrak).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Nokontrak = param.Nokontrak;
                data.Idphk3 = param.Idphk3;
                data.Idkeg = param.Idkeg;
                data.Tglkontrak = param.Tglkontrak;
                data.Tglakhirkontrak = param.Tglakhirkontrak;
                data.Uraian = param.Uraian;
                data.Nilai = param.Nilai;
                data.Dateupdate = param.Dateupdate;
                _tukdContext.Kontrak.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
