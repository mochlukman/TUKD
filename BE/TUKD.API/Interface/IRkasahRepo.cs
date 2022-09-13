using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IRkasahRepo : IRepo<Rkasah>
    {
        Task<PrimengTableResult<RkasahView>> Paging(PrimengTableParam<RkaGlobalGet> param);
        Task<RkasahView> ViewData(long Idrkasah);
        Task<bool> Update(Rkasah param);
    }
}
