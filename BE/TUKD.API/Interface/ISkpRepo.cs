using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface ISkpRepo : IRepo<Skp>
    {
        Task<List<Skp>> ViewDatas(long Idunit, long? Idbend, long Idxkode, string Kdstatus, bool istglvalid = false);
        Task<Skp> ViewData(long Idskp);
        Task<bool> Update(Skp param);
    }
}
