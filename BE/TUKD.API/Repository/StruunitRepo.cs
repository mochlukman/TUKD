using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class StruunitRepo : Repo<Struunit>, IStruunitRepo
    {
        public StruunitRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<List<Struunit>> ViewDatas()
        {
            return await _tukdContext.Struunit.ToListAsync();
        }
    }
}
