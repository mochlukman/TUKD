using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IBpkpajakdetRepo : IRepo<Bpkpajakdet>
    {
        Task<List<Bpkpajakdet>> ViewDatas(BpkpajakdetGet param);
        Task<Bpkpajakdet> ViewData(long Idbpkpajakdet);
        Task<bool> Update(Bpkpajakdet param);
        Task<decimal?> sumNilai(long Idbpkpajak);
    }
}
