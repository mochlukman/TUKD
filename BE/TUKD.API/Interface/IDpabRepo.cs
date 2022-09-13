using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IDpabRepo : IRepo<Dpab>
    {
        Task<List<DpabView>> GetByStsdetd(long Idunit, long Idsts);
        Task<bool> UpdateNilai(Dpadetb param, decimal? newTotal);
        Task<decimal?> GetNilai(long Iddpab);
    }
}
