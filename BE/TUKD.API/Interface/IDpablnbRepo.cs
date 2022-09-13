using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IDpablnbRepo : IRepo<Dpablnb>
    {
        Task<decimal?> TotalNilai(long Iddpab);
        Task<bool> Update(DpablnbView param);
    }
}
