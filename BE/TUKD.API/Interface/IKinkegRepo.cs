using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IKinkegRepo : IRepo<Kinkeg>
    {
        Task<List<KinkegView>> ViewDatas(long Idkegunit, string Kdjkk);
        Task<KinkegView> ViewData(long Idkinkeg);
        Task<PrimengTableResult<KinkegView>> Paging(PrimengTableParam<KinkegGet> param);
        Task<bool> Update(Kinkeg param);
    }
}
