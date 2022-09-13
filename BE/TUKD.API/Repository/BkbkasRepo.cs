using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class BkbkasRepo : Repo<Bkbkas>, IBkbkasRepo
    {
        public BkbkasRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Bkbkas param)
        {
            Bkbkas data = await _tukdContext.Bkbkas.Where(w => w.Nobbantu.Trim() == param.Nobbantu.Trim()).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Idunit = param.Idunit;
                data.Idrek = param.Idrek;
                data.Idbank = param.Idbank;
                data.Nmbkas = param.Nmbkas;
                data.Norek = param.Norek;
                data.Saldo = param.Saldo;
                _tukdContext.Bkbkas.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
