using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IRkatapdrRepo : IRepo<Rkatapdr>
    {
        Task<string> GenerateNomor(long Idrka);
        Task<PrimengTableResult<RkatapdrView>> Paging(PrimengTableParam<RkatapdGet> param);
        Task<RkatapdrView> ViewData(long Idtapd);
        Task<bool> Update(Rkatapdr param);
    }
}
