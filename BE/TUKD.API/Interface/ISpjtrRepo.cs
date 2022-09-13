using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface ISpjtrRepo : IRepo<Spjtr>
    {
        Task<Spjtr> ViewData(long Idspjtr);
        Task<List<Spjtr>> ViewDatas(SpjGetsParam param);
        Task<PrimengTableResult<Spjtr>> Paging(PrimengTableParam<SpjGetsParam> param);
        Task<bool> Update(Spjtr param);
    }
}
