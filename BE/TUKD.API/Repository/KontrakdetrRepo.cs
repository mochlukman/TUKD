using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class KontrakdetrRepo : Repo<Kontrakdetr>, IKontrakdetrRepo
    {
        public KontrakdetrRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Kontrakdetr param)
        {
            Kontrakdetr data = await _tukdContext.Kontrakdetr.Where(w => w.Iddetkontrak == param.Iddetkontrak).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Idrek = param.Idrek;
                data.Nilai = param.Nilai;
                data.Idbulan = param.Idbulan;
                data.Idjtermorlun = param.Idjtermorlun;
                data.Dateupdate = param.Dateupdate;
                _tukdContext.Kontrakdetr.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
