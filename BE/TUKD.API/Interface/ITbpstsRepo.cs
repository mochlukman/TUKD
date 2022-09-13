using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface ITbpstsRepo : IRepo<Tbpsts>
    {
        Task<List<Tbpsts>> GetByTbp(long Idtbp);
        Task<List<Tbpsts>> GetBySts(long Idsts);
        Task<Tbpsts> ViewData(long Idtbp, long Idsts);
    }
}
