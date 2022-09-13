using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IRkabRepo : IRepo<Rkab>
    {
        Task<PrimengTableResult<RkabView>> Paging(PrimengTableParam<RkaGlobalGet> param);
        Task<List<RkabView>> ViewDatas(RkaGlobalGet param);
        Task<RkabView> ViewData(long Idrkab);
        Task<bool> Update(Rkab param);
        void CalculateNilai(long Idrkab);
        Task<decimal?> TotalNilai(long Idunit, int? Trkr);
    }
}
