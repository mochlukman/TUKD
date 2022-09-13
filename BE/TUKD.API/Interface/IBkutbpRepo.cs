using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IBkutbpRepo : IRepo<Bkutbp>
    {
        Task<List<Bkutbp>> ViewDatas(long Idunit, long Idbend);
        Task<List<Bkutbp>> ViewDatasForSpjtr(long Idspjtr, long Idunit, long Idbend);
    }
}
