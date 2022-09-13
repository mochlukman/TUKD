using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface ISpjsppRepo : IRepo<Spjspp>
    {
        Task<List<SpjsppView>> GetBySpp(long Idspp, string Kdstatus);
    }
}
