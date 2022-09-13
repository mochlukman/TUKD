using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IPanjardetRepo : IRepo<Panjardet>
    {
        Task<List<PanjardetView>> ViewDatas(long Idpanjar);
        Task<PanjardetView> ViewData(long Idpanjardet);
        Task<decimal?> TotalNilai(long Idpanjar);
        Task<decimal?> TotalNilaiPanjar(List<long> Idpanjar);
        Task<bool> Update(Panjardet param);
    }
}
