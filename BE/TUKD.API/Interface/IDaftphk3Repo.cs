using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IDaftphk3Repo : IRepo<Daftphk3>
    {
        Task<bool> Update(Daftphk3 param);
    }
}
