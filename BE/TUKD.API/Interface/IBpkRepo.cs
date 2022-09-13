using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IBpkRepo : IRepo<Bpk>
    {
        Task<PrimengTableResult<Bpk>> Paging(PrimengTableParam<BpkGet> param);
        Task<List<Bpk>> ViewDatas(BpkGet param);
        Task<Bpk> ViewData(long Idbpk);
        Task<List<BpkView>> DataViewsOld(BpkGet param);
        Task<BpkView> DataViewOld(long Idbpk);
        Task<bool> Update(Bpk param);
        Task<bool> Pengesahan(Bpk param);
    }
}
