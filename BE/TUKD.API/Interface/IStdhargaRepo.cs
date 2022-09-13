using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IStdhargaRepo : IRepo<Stdharga>
    {
        Task<PrimengTableResult<Stdharga>> Paging(PrimengTableParam<Stdharga> param);
    }
}
