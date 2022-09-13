using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TUKD.API.Interface;
using TUKD.API.Models;

namespace TUKD.API.Repository
{
    public class BkbankdetRepo : Repo<Bkbankdet>, IBkbankdetRepo
    {
        public BkbankdetRepo(DbContext context) : base(context)
        {
        }
        public TukdContext _tukdContext => _context as TukdContext;

        public async Task<decimal?> TotalNilaiGeser(List<long> Idbkbank)
        {
            decimal? total = await _tukdContext.Bkbankdet.Where(w => Idbkbank.Contains(w.Idbkbank)).SumAsync(s => s.Nilai);
            return total;
        }

        public async Task<bool> Update(Bkbankdet param)
        {
            Bkbankdet data = await _tukdContext.Bkbankdet.Where(w => w.Idbankdet == param.Idbankdet).FirstOrDefaultAsync();
            if (data == null) return false;
            data.Nilai = param.Nilai;
            _tukdContext.Bkbankdet.Update(data);
            if (await _tukdContext.SaveChangesAsync() > 0) return true;
            return false;
        }

        public async Task<Bkbankdet> ViewData(long Idbankdet)
        {
            Bkbankdet data = await(
                from det in _tukdContext.Bkbankdet
                join bk in _tukdContext.Bkbank on det.Idbkbank equals bk.Idbkbank
                join status in _tukdContext.Stattrs on bk.Kdstatus.Trim() equals status.Kdstatus.Trim()
                where det.Idbankdet == Idbankdet
                select new Bkbankdet
                {
                    Idbankdet = det.Idbankdet,
                    Idbkbank = det.Idbkbank,
                    Nilai = det.Nilai,
                    Idnojetra = det.Idnojetra,
                    IdbkbankNavigation = bk ?? null
                }
                ).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<Bkbankdet>> ViewDatas(long Idbkbank)
        {
            List<Bkbankdet> datas = await (
                from det in _tukdContext.Bkbankdet
                join bk in _tukdContext.Bkbank on det.Idbkbank equals bk.Idbkbank
                join status in _tukdContext.Stattrs on bk.Kdstatus.Trim() equals status.Kdstatus.Trim()
                where det.Idbkbank == Idbkbank
                select new Bkbankdet
                {
                    Idbankdet = det.Idbankdet,
                    Idbkbank = det.Idbkbank,
                    Nilai = det.Nilai,
                    Idnojetra = det.Idnojetra,
                    IdbkbankNavigation = bk ?? null
                }
                ).ToListAsync();
            return datas;
        }
    }
}
