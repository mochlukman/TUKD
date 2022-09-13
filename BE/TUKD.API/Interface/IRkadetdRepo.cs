using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IRkadetdRepo : IRepo<Rkadetd>
    {
        Task<PrimengTableResult<Rkadetd>> Paging(PrimengTableParam<RkadetdGet> param);
        Task<List<Rkadetd>> ViewDatas(RkadetdGet param);
        Task<Rkadetd> ViewData(long Idrkadetd);
        Task<bool> Update(Rkadetd param);
        void UpdateToHeader(long Idrkadetd);
        void GetLastChild(long Idrkadetd);
    }
}
