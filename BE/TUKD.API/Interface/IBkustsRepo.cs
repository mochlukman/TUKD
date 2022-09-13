using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IBkustsRepo : IRepo<Bkusts>
    {
        Task<List<Bkusts>> ViewDatas(long Idunit, long Idbend);
        Task<List<Bkusts>> ViewDatasForSpjtr(long Idspjtr, long Idunit, long Idbend);
    }
}
