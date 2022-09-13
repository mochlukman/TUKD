using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class WebsetRepo : Repo<Webset>, IWebsetRepo
    {
        public WebsetRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Webset param)
        {
            Webset data = await _tukdContext.Webset.Where(w => w.Idwebset == param.Idwebset && w.Kdset.Trim() == param.Kdset.Trim()).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Valset = param.Valset;
            _tukdContext.Webset.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0)
                return true;
            return false;
        }
    }
}
