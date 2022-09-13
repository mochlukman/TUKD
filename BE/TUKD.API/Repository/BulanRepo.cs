using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class BulanRepo : Repo<Bulan>, IBulanRepo
    {
        public BulanRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Bulan param)
        {
            Bulan data = await _tukdContext.Bulan.Where(w => w.Idbulan == param.Idbulan).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Kdperiode = param.Kdperiode;
            data.KetBulan = param.KetBulan;
            _tukdContext.Bulan.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }
    }
}
