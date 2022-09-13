using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface ISaldoawallraRepo : IRepo<Saldoawallra>
    {
        Task<List<Saldoawallra>> ViewDatas(long Idunit, long Idjnsakun);
        Task<Saldoawallra> ViewData(long Idsaldo);
        Task<bool> Update(Saldoawallra param);
    }
}
