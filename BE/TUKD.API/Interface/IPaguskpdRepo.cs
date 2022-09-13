using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IPaguskpdRepo : IRepo<Paguskpd>
    {
        Task<PrimengTableResult<PaguskpdView>> Paging(PrimengTableParam<PaguskpdGet> param);
        Task<PaguskpdView> ViewData(long Idpaguskpd);
        Task<List<PaguskpdView>> ViewDatas(PaguskpdGet param);
        Task<bool> Update(Paguskpd param);
    }
}
