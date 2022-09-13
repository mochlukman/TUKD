using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface ISppcheckdokRepo : IRepo<Sppcheckdok>
    {
        Task<List<Sppcheckdok>> ViewDatas(SppcheckdokGet param);
        Task<Sppcheckdok> ViewData(long Idspp, long Idcheck);
    }
}
