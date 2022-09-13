using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IBpkspjRepo : IRepo<Bpkspj>
    {
        Task<BpkspjView> ViewData(long Idbpkspj);
        Task<List<BpkspjView>> BySpj(long Idspj);
        Task<List<BpkspjView>> ByBpk(long Idbpk);
    }
}
