using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class SpdRepo : Repo<Spd>, ISpdRepo
    {
        public SpdRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<List<long>> getIds(long Idunit)
        {
            List<long> Ids = await _tukdContext.Spd.Where(w => w.Idunit == Idunit).Select(s => s.Idspd).ToListAsync();
            return Ids;
        }

        public async Task<bool> Pengesahan(Spd param)
        {
            Spd data = await _tukdContext.Spd.Where(w => w.Idspd == param.Idspd).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Tglvalid = param.Tglvalid;
                data.Valid = param.Valid;
                data.Dateupdate = param.Dateupdate;
                _tukdContext.Spd.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }

        public async Task<bool> Update(Spd param)
        {
            Spd data = await _tukdContext.Spd.Where(w => w.Idspd == param.Idspd).FirstOrDefaultAsync();
            data.Nospd = param.Nospd;
            data.Dateupdate = param.Dateupdate;
            data.Tglspd = param.Tglspd;
            data.Idbulan1 = param.Idbulan1;
            data.Idbulan2 = param.Idbulan2;
            data.Keterangan = param.Keterangan;
            data.Idttd = param.Idttd;
            _tukdContext.Spd.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }
    }
}
