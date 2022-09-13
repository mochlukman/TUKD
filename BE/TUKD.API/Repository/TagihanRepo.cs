using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class TagihanRepo : Repo<Tagihan>, ITagihanRepo
    {
        public TagihanRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Tagihan param)
        {
            Tagihan data = await _tukdContext.Tagihan.Where(w => w.Idtagihan == param.Idtagihan).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Notagihan = param.Notagihan;
                data.Idkeg = param.Idkeg;
                data.Tgltagihan = param.Tgltagihan;
                data.Idkontrak = param.Idkontrak;
                data.Uraiantagihan = param.Uraiantagihan;
                data.Tglvalid = param.Tglvalid;
                data.Kdstatus = param.Kdstatus;
                data.Dateupdate = param.Dateupdate;
                _tukdContext.Tagihan.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
