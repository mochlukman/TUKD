using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IBkbankRepo : IRepo<Bkbank>
    {
        Task<List<Bkbank>> ViewDatas(BkbankGet param);
        Task<Bkbank> ViewData(long Idbkbank);
        Task<List<long>> GetIds(long Idunit,string Kdstatus, long Idbend);
        Task<bool> Update(Bkbank param);
    }
}
