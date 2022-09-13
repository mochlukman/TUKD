using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IRkatapddetbRepo : IRepo<Rkatapddetb>
    {
        Task<string> GenerateNomor(long Idrka);
        Task<PrimengTableResult<RkatapddetbView>> Paging(PrimengTableParam<RkatapddetGet> param);
        Task<RkatapddetbView> ViewData(long Idtapddet);
        Task<bool> Update(Rkatapddetb param);
    }
}
