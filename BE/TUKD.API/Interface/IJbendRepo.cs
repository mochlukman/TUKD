using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IJbendRepo : IRepo<Jbend>
    {
        Task<bool> Update(Jbend param);
    }
}
