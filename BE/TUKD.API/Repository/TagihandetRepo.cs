using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class TagihandetRepo : Repo<Tagihandet>, ITagihandetRepo
    {
        public TagihandetRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
        public async Task<bool> Update(Tagihandet param)
        {
            Tagihandet data = await _tukdContext.Tagihandet.Where(w => w.Idtagihandet == param.Idtagihandet).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Idrek = param.Idrek;
                data.Nilai = param.Nilai;
                data.Dateupdate = param.Dateupdate;
                _tukdContext.Tagihandet.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
