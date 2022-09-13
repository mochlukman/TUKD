using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IBkbankdetRepo : IRepo<Bkbankdet>
    {
        Task<List<Bkbankdet>> ViewDatas(long Idbkbank);
        Task<Bkbankdet> ViewData(long Idbankdet);
        Task<decimal?> TotalNilaiGeser(List<long> Idbkbank);
        Task<bool> Update(Bkbankdet param);
    }
}
