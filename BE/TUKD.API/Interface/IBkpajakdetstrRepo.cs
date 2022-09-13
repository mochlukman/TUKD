using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IBkpajakdetstrRepo : IRepo<Bkpajakdetstr>
    {
        Task<List<Bkpajakdetstr>> ViewDatas(long Idbkpajak);
        Task<Bkpajakdetstr> ViewData(long Idbkpajakdetstr);
        Task<bool> Update(Bkpajakdetstr param);
    }
}
