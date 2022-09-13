using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class JtransferRepo : Repo<Jtransfer>, IJtransferRepo
    {
        public JtransferRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Jtransfer param)
        {
            Jtransfer data = await _tukdContext.Jtransfer.Where(w => w.Idjtransfer == param.Idjtransfer).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Minnominal = param.Minnominal;
            data.Nmtransfer = param.Nmtransfer;
            data.Uraiantrans = param.Uraiantrans;
            data.Flagsnom = param.Flagsnom;
            _tukdContext.Jtransfer.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }
    }
}
