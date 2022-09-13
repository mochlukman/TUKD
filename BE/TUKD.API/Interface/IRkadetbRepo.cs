using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IRkadetbRepo : IRepo<Rkadetb>
    {
        Task<PrimengTableResult<Rkadetb>> Paging(PrimengTableParam<RkadetbGet> param);
        Task<List<Rkadetb>> ViewDatas(RkadetbGet param);
        Task<Rkadetb> ViewData(long Idrkadetb);
        Task<bool> Update(Rkadetb param);
        void UpdateToHeader(long Idrkadetb);
        void GetLastChild(long Idrkadetb);
    }
}
