using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface ITahapRepo : IRepo<Tahap>
    {
        Task<PrimengTableResult<Tahap>> Paging(PrimengTableParam<TahapGet> param);
        Task<bool> Update(Tahap param);
        Task<bool> Lock(Tahap param);
    }
}
