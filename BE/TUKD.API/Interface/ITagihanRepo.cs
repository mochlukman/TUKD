using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface ITagihanRepo : IRepo<Tagihan>
    {
        Task<bool> Update(Tagihan param);
    }
}
