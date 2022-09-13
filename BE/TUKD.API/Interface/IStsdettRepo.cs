using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IStsdettRepo : IRepo<Stsdett>
    {
        Task<List<Stsdett>> ViewDatas(long Idsts);
        Task<Stsdett> ViewData(long Idstsdett);
        Task<bool> Update(Stsdett param);
    }
}
