using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface ITbpRepo : IRepo<Tbp>
    {
        Task<List<Tbp>> ViewDatas(long Idunit, List<string> Kdstatus, int Idxkode, long? Idbend, bool Isvalid = false);
        Task<List<Tbp>> ViewDatas(long Idunit, int Idxkode);
        Task<Tbp> ViewData(long Idtbp);
        Task<List<long>> GetIds(long Idtbp);
        Task<bool> Update(Tbp param);
    }
}
