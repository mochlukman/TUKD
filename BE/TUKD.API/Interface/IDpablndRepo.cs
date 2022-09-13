using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IDpablndRepo : IRepo<Dpablnd>
    {
        Task<decimal?> TotalNilai(long Iddpad);
        Task<bool> Update(DpablndView param);
    }
}
