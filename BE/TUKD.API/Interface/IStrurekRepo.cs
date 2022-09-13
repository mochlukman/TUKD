using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IStrurekRepo : IRepo<Strurek>
    {
        Task<bool> Update(Strurek param);
    }
}
