using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IBktmemRepo : IRepo<Bktmem>
    {
        Task<List<Bktmem>> ViewDatas(long Idunit, string Kdbm);
        Task<Bktmem> ViewData(long Idbm);
        Task<bool> Update(Bktmem param);
    }
}
