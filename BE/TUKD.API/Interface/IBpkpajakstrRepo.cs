using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IBpkpajakstrRepo : IRepo<Bpkpajakstr>
    {
        Task<List<BpkpajakstrView>> ViewDatas(BpkpajakstrGet param);
        Task<BpkpajakstrView> ViewData(long Idbpkpajakstr);
        Task<bool> Update(Bpkpajakstr param);
    }
}
