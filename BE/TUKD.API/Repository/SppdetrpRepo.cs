using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class SppdetrpRepo : Repo<Sppdetrp>, ISppdetrpRepo
    {
        public SppdetrpRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Sppdetrp param)
        {
            Sppdetrp data = await _tukdContext.Sppdetrp.Where(w => w.Idsppdetrp == param.Idsppdetrp).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Nilai = param.Nilai;
                data.Keterangan = param.Keterangan;
                data.Idbilling = param.Idbilling;
                data.Tglbilling = param.Tglbilling;
                data.Ntpn = param.Ntpn;
                data.Ntb = param.Ntb;
                data.Updatedate = param.Updatedate;
                data.Updateby = param.Updateby;
                _tukdContext.Sppdetrp.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
