using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class DpaprogramRepo : Repo<Dpaprogram>, IDpaprogramRepo
    {
        public DpaprogramRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
    }
}
