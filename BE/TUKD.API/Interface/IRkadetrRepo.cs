using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IRkadetrRepo : IRepo<Rkadetr>
    {
        Task<PrimengTableResult<Rkadetr>> Paging(PrimengTableParam<RkadetrGet> param);
        Task<List<Rkadetr>> ViewDatas(RkadetrGet param);
        Task<Rkadetr> ViewData(long Idrkadetr);
        Task<bool> Update(Rkadetr param);
        void UpdateToHeader(long Idrkadetr);
        void GetLastChild(long Idrkadetr);
    }
}
