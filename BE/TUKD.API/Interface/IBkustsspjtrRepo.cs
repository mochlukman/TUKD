using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IBkustsspjtrRepo : IRepo<Bkustsspjtr>
    {
        Task<List<BkustsspjtrView>> ViewDatas(long Idspjtr);
        Task<BkustsspjtrView> ViewData(long Idbkustsspjtr);
    }
}
