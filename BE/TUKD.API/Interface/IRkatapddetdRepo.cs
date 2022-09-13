using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IRkatapddetdRepo : IRepo<Rkatapddetd>
    {
        Task<string> GenerateNomor(long Idrka);
        Task<PrimengTableResult<RkatapddetdView>> Paging(PrimengTableParam<RkatapddetGet> param);
        Task<RkatapddetdView> ViewData(long Idtapddet);
        Task<bool> Update(Rkatapddetd param);
    }
}
