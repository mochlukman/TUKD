using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IDpablnrRepo : IRepo<Dpablnr>
    {
        Task<decimal?> TotalNilai(long Iddpar);
        Task<bool> Update(DpablnrView param);
    }
}
