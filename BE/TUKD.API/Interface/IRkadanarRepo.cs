using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IRkadanarRepo : IRepo<Rkadanar>
    {
        Task<RkadanarView> ViewData(long Idrkadana);
        Task<List<RkadanarView>> ViewDatas(long Idrka);
        Task<bool> Update(Rkadanar param);

    }
}
