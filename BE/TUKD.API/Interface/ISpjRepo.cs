using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface ISpjRepo : IRepo<Spj>
    {
        Task<Spj> ViewData(long Idspj);
        Task<List<Spj>> ViewDatas(SpjGetsParam param);
        Task<List<Spj>> ForLpj(SpjGetsForLpjParam param);
        Task<PrimengTableResult<Spj>> Paging(PrimengTableParam<SpjGetsParam> param);
        Task<bool> Update(Spj param);
        Task<bool> Pengesahan(Spj param);
        Task<List<SpjLookupForSpp>> LookupForSpp(long Idunit, long Idspp, string Kdsatus);
    }
}
