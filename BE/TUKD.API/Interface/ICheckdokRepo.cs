using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface ICheckdokRepo : IRepo<Checkdok>
    {
        Task<List<Checkdok>> ViewDatas(CheckdokGet param);
        Task<Checkdok> ViewData(long Idcheck);
        Task<bool> Update(Checkdok param);
    }
}
