using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class JsatuanRepo : Repo<Jsatuan>, IJsatuanRepo
    {
        public JsatuanRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Jsatuan param)
        {
            Jsatuan data = await _tukdContext.Jsatuan.Where(w => w.Idsatuan == param.Idsatuan).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Kdsatuan = param.Kdsatuan;
            data.Uraisatuan = param.Uraisatuan;
            _tukdContext.Jsatuan.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }
    }
}
