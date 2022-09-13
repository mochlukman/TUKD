using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IRkadRepo : IRepo<Rkad>
    {
        Task<PrimengTableResult<RkadView>> Paging(PrimengTableParam<RkaGlobalGet> param);
        Task<List<RkadView>> ViewDatas(RkaGlobalGet param);
        Task<RkadView> ViewData(long Idrkad);
        Task<bool> Update(Rkad param);
        void CalculateNilai(long Idrkad);
        Task<decimal?> TotalNilai(long Idunit, long? Idrkadx);
    }
}
