using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class JnsakunRepo : Repo<Jnsakun>, IJnsakunRepo
    {
        public JnsakunRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _ctx => _context as TukdContext;
    }
}
