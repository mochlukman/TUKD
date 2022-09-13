using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IGolonganRepo : IRepo<Golongan>
    {
        Task<List<Golongan>> ViewDatas(GolonganGet param);
        Task<Golongan> ViewData(long Idgol);
        Task<PrimengTableResult<Golongan>> Paging(PrimengTableParam<GolonganGet> param);
        Task<bool> Update(Golongan param);
    }
}
