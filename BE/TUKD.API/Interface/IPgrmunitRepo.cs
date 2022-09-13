using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IPgrmunitRepo : IRepo<Pgrmunit>
    {
        Task<List<PgrmunitView>> ViewDatas(PgrmunitGet param);
        Task<PgrmunitView> ViewData(long Idpgrmunit);
        Task<PrimengTableResult<PgrmunitView>> Paging(PrimengTableParam<PgrmunitGet> param);
        Task<bool> Update(Pgrmunit param);
    }
}
