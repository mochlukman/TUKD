using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IDpaRepo : IRepo<Dpa>
    {
        Task<PrimengTableResult<Dpa>> Paging(PrimengTableParam<DpaGet> param);
        Task<List<Dpa>> ViewDatas(DpaGet param);
        Task<Dpa> ViewData(long Iddpa);
        Task<bool> Update(Dpa param);
        Task<bool> UpdateNilai(Dpa param);
        Task<List<long>> GetIdunits();
        Task<List<long>> GetIds(long Idunit, string kdtahap = null);
        Task<bool> Pengesahan(Dpa param);
        Task<bool> Penolakan(Dpa param);
    }
}
