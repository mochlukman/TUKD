using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IMpgrmRepo : IRepo<Mpgrm>
    {
        Task<List<LookupTree>> Tree(long Idurus);
        Task<PrimengTableResult<Mpgrm>> Paging(PrimengTableParam<MpgrmGet> param);
        Task<List<Mpgrm>> ViewDatas(MpgrmGet param);
        Task<Mpgrm> ViewData(long Idprgrm);
        Task<bool> Update(Mpgrm param);
    }
}
