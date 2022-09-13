using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface ITbpdettRepo : IRepo<Tbpdett>
    {
        Task<List<Tbpdett>> ViewDatas(long Idtbp);
        Task<Tbpdett> ViewData(long Idtbpdett);
        Task<decimal?> TotalNilaiPelimpahan(List<long> Idtbp);
        Task<bool> Update(Tbpdett param);
    }
}
