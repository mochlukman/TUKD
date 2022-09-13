using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface ILpjRepo : IRepo<Lpj>
    {
        Task<Lpj> ViewData(long Idlpj);
        Task<PrimengTableResult<Lpj>> Paging(PrimengTableParam<LpjGetsParam> param);
        Task<bool> Update(Lpj param);
        Task<bool> Pengesahan(Lpj param);
    }
}
