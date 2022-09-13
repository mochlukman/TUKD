using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface ISp2dcheckdokRepo : IRepo<Sp2dcheckdok>
    {
        Task<List<Sp2dcheckdok>> ViewDatas(Sp2dcheckdokGet param);
        Task<Sp2dcheckdok> ViewData(long Idsp2d, long Idcheck);
    }
}
