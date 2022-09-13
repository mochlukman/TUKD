using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IKegsbdanaRepo : IRepo<Kegsbdana>
    {
        Task<List<KegsbdanaView>> ViewDatas(long Idkegunit);
        Task<KegsbdanaView> ViewData(long Idkegdana);
        Task<bool> Update(Kegsbdana param);
    }
}
