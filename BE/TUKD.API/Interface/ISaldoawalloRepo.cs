using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface ISaldoawalloRepo : IRepo<Saldoawallo>
    {
        Task<List<Saldoawallo>> ViewDatas(long Idunit, long Idjnsakun);
        Task<Saldoawallo> ViewData(long Idsaldo);
        Task<bool> Update(Saldoawallo param);
    }
}
