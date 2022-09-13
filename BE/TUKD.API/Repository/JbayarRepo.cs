using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class JbayarRepo : Repo<Jbayar>, IJbayarRepo
    {
        public JbayarRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Jbayar param)
        {
            Jbayar data = await _tukdContext.Jbayar.Where(w => w.Idjbayar == param.Idjbayar).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Kdbayar = param.Kdbayar;
            data.Uraianbayar = param.Uraianbayar;
            data.Dateupdate = param.Dateupdate;
            _tukdContext.Jbayar.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }
    }
}
