using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IKegunitRepo : IRepo<Kegunit>
    {
        Task<List<LookupTree>> Tree(long Idunit, string kdtahap, string type);
        Task<List<long>> IdskegByUnit(long Idunit);
        Task<PrimengTableResult<KegunitView>> Paging(PrimengTableParam<KegunitGet> param);
        Task<List<KegunitView>> ViewDatas(KegunitGet param);
        Task<KegunitView> ViewData(long Idkegunit);
        Task<bool> Update(Kegunit param);
    }
}
