using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IStsdetdRepo : IRepo<Stsdetd>
    {
        Task<List<Stsdetd>> ViewDatas(long Idsts);
        Task<Stsdetd> ViewData(long Idstsdetd);
        Task<bool> Update(Stsdetd param);
    }
}
