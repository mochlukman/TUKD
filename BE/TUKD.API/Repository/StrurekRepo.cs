using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class StrurekRepo : Repo<Strurek>, IStrurekRepo
    {
        public StrurekRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<bool> Update(Strurek param)
        {
            Strurek data = await _tukdContext.Strurek.Where(w => w.Idstrurek == param.Idstrurek).FirstOrDefaultAsync();
            if(data != null)
            {
                data.Nmlevel = param.Nmlevel;
                _tukdContext.Strurek.Update(data);
                if (await _tukdContext.SaveChangesAsync() > 0)
                    return true;
                return false;
            }
            return false;
        }
    }
}
