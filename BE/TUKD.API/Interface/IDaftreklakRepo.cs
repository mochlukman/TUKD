using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IDaftreklakRepo : IRepo<Daftreklak>
    {
        Task<List<Daftreklak>> ViewDatas(List<int?> Idjnslak);
        Task<Daftreklak> ViewData(long Idrek);
        Task<bool> Update(Daftreklak param);
    }
}
