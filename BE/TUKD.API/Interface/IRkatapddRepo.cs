using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IRkatapddRepo : IRepo<Rkatapdd>
    {
        Task<string> GenerateNomor(long Idrka);
        Task<PrimengTableResult<RkatapddView>> Paging(PrimengTableParam<RkatapdGet> param);
        Task<RkatapddView> ViewData(long Idtapd);
        Task<bool> Update(Rkatapdd param);
    }
}
