using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface ISpddetbRepo : IRepo<Spddetb>
    {
        Task<decimal?> TotalNilaiSpd(long Idspd);
        Task<List<long>> GetIdReks(long Idspd, long Idkeg);
    }
}
