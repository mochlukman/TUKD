using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class Daftphk3Repo : Repo<Daftphk3>, IDaftphk3Repo
    {
        public Daftphk3Repo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Daftphk3 param)
        {
            Daftphk3 data = await _tukdContext.Daftphk3.Where(w => w.Idphk3 == param.Idphk3).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Nmphk3 = param.Nmphk3;
                data.Nminst = param.Nminst;
                data.Idbank = param.Idbank;
                data.Cabangbank = param.Cabangbank;
                data.Alamatbank = param.Alamatbank;
                data.Norekbank = param.Norekbank;
                data.Idjusaha = param.Idjusaha;
                data.Alamat = param.Alamat;
                data.Telepon = param.Telepon;
                data.Npwp = param.Npwp;
                data.Warganegara = param.Warganegara;
                data.Stpenduduk = param.Stpenduduk;
                data.Dateupdate = param.Dateupdate;
                _tukdContext.Daftphk3.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
