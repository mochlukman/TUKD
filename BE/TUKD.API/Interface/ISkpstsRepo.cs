using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface ISkpstsRepo : IRepo<Skpsts>
    {
        Task<Skpsts> ViewData(long Idskp, long Idsts);
    }
}
