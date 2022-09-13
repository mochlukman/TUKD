using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IRkatapddetrRepo : IRepo<Rkatapddetr>
    {
        Task<string> GenerateNomor(long Idrka);
        Task<PrimengTableResult<RkatapddetrView>> Paging(PrimengTableParam<RkatapddetGet> param);
        Task<RkatapddetrView> ViewData(long Idtapddet);
        Task<bool> Update(Rkatapddetr param);
    }
}
