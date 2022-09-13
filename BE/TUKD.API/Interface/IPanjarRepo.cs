using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IPanjarRepo : IRepo<Panjar>
    {
        Task<List<Panjar>> ViewDatas(PanjarGet param);
        Task<Panjar> ViewData(long Idpanjar);
        Task<List<long>> GetIds(long Idpanjar);
        Task<bool> Update(Panjar param);
    }
}
