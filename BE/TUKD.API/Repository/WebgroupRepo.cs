using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class WebgroupRexpo : Repo<Webgroup>, IWebgroupRepo
    {
        public WebgroupRexpo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Webgroup param)
        {
            Webgroup data = await _tukdContext.Webgroup.Where(w => w.Groupid == param.Groupid).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Nmgroup = param.Nmgroup;
                data.Ket = param.Ket;
                _tukdContext.Webgroup.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
