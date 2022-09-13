using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IDpadRepo : IRepo<Dpad>
    {
        Task<List<DpadView>> ViewDatas(DpaRekGet param);
        Task<DpadView> ViewData(long Iddpad);
        Task<bool> Update(Dpad param);
        Task<bool> UpdateNilai(Dpadetd param, decimal? newTotal);
        Task<decimal?> GetNilai(long Iddpad);
    }
}
