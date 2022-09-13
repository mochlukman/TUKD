using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IDaftunitRepo : IRepo<Daftunit>
    {
        Task<PrimengTableResult<DaftunitView>> Paging(PrimengTableParam<DaftunitGet> param);
        Task<List<DaftunitView>> ViewDatas(DaftunitGet param);
        Task<DaftunitView> ViewData(long Idunit);
        Task<bool> Update(Daftunit param);
    }
}
