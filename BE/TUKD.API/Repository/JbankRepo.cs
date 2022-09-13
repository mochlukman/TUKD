using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class JbankRepo : Repo<Jbank>, IJbankRepo
    {
        public JbankRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Jbank param)
        {
            Jbank data = await _tukdContext.Jbank.Where(w => w.Idbank == param.Idbank).FirstOrDefaultAsync();
            if (data != null)
            {
                data.Kdbank = param.Kdbank;
                data.Nmbank = param.Nmbank;
                data.Uraian = param.Uraian;
                data.Akronim = param.Akronim;
                data.Alamat = param.Alamat;
                data.Datecreate = param.Datecreate;
                _tukdContext.Jbank.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false; ;
        }
    }
}
