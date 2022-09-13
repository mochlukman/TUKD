using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class ZkodeRepo : Repo<Zkode>, IZkodeRepo
    {
        public ZkodeRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Zkode param)
        {
            Zkode data = await _tukdContext.Zkode.Where(w => w.Idxkode == param.Idxkode).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Uraian = param.Uraian;
                _tukdContext.Zkode.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
