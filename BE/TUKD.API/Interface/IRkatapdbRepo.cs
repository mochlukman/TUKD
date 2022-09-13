using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IRkatapdbRepo : IRepo<Rkatapdb>
    {
        Task<string> GenerateNomor(long Idrka);
        Task<PrimengTableResult<RkatapdbView>> Paging(PrimengTableParam<RkatapdGet> param);
        Task<RkatapdbView> ViewData(long Idtapd);
        Task<bool> Update(Rkatapdb param);
    }
}
