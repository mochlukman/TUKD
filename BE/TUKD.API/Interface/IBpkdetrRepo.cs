using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IBpkdetrRepo : IRepo<Bpkdetr>
    {
        Task<PrimengTableResult<Bpkdetr>> Paging(PrimengTableParam<BpkdetrGet> param);
        Task<List<Bpkdetr>> ViewDatas(BpkdetrGet param);
        Task<Bpkdetr> ViewData(long Idbpkdetr);
        Task<bool> Update(Bpkdetr param);
    }
}
