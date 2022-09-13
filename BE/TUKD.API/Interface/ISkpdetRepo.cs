using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface ISkpdetRepo : IRepo<Skpdet>
    {
        Task<List<Skpdet>> ViewDatas(long Idskp);
        Task<Skpdet> ViewData(long Idskpdet);
        Task<bool> Update(Skpdet param);
    }
}
