using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IPajakRepo : IRepo<Pajak>
    {
        Task<bool> Update(Pajak param);
        Task<List<Pajak>> GetBySppdetr(long Idsppdetr);
        Task<List<Pajak>> ViewDatas(PajakGet param);
        Task<Pajak> ViewData(long Idpajak);

    }
}
