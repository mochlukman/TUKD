using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IJkinkegRepo : IRepo<Jkinkeg>
    {
        Task<bool> Update(Jkinkeg param);
        Task<PrimengTableResult<Jkinkeg>> Paging(PrimengTableParam<Jkinkeg> param);
    }
}
