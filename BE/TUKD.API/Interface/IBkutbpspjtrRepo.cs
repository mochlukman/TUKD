using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IBkutbpspjtrRepo : IRepo<Bkutbpspjtr>
    {
        Task<List<BkutbpspjtrView>> ViewDatas(long Idspjtr);
        Task<BkutbpspjtrView> ViewData(long Idbkutbpspjtr);
    }
}
