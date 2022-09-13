using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class PrognosisbRepo : Repo<Prognosisb>, IPrognosisbRepo
    {
        public PrognosisbRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _c => _context as TukdContext;
    }
}
