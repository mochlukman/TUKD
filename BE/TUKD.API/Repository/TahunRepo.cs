using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class TahunRepo : Repo<Tahun>, ITahunRepo
    {
        public TahunRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;
        public string GetNamaTahun(string kdtahun)
        {
            var nmtahun = _tukdContext.Tahun.Where(w => w.Kdtahun.Trim() == kdtahun.Trim()).Select(s => s.Nmtahun).FirstOrDefault();
            return nmtahun;
        }
        public async Task<string> GetKdtahun()
        {
            var tahun = await _tukdContext.Tahun.OrderBy(o => o.Kdtahun).Select(s => s.Kdtahun).LastOrDefaultAsync();
            if (tahun == null)
                return "1";
            var toNumber = Int32.Parse(tahun);
            var PlusNumber = toNumber + 1;
            string NewKode = PlusNumber.ToString();
            return NewKode;
        }
        public async Task<List<Tahun>> Search(string Keyword)
        {
            List<Tahun> listTahun = await _tukdContext.Tahun.Where(w => w.Kdtahun.Trim().Contains(Keyword) || w.Nmtahun.Trim().Contains(Keyword)).ToListAsync();
            return listTahun;
        }
    }
}
