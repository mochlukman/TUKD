using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IDpRepo : IRepo<Dp>
    {
        Task<string> GenerateNo();
        Task<PrimengTableResult<Dp>> Paging(PrimengTableParam<DpGet> param);
        Task<bool> Update(Dp param);
    }
}
