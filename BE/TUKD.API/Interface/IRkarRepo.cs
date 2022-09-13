using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IRkarRepo : IRepo<Rkar>
    {
        Task<PrimengTableResult<RkarView>> Paging(PrimengTableParam<RkaGlobalGet> param);
        Task<List<RkarView>> ViewDatas(RkaGlobalGet param);
        Task<RkarView> ViewData(long Idrkar);
        Task<bool> Update(Rkar param);
        void CalculateNilai(long Idrkar);
        Task<decimal?> TotalNilai(long Idunit);
        Task<decimal?> TotalNilaiKeg(long Idunit, long Idkeg);
    }
}
