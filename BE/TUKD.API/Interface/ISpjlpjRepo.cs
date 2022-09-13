using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface ISpjlpjRepo : IRepo<Spjlpj>
    {
        Task<SpjlpjView> ViewData(long Idspjlpj);
        Task<List<SpjlpjView>> ViewDatas(long Idlpj);
    }
}
