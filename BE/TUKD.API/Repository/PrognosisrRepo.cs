using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class PrognosisrRepo : Repo<Prognosisr>, IPrognosisrRepo
    {
        public PrognosisrRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _c => _context as TukdContext;
    }
}
