using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface ISpptagRepo : IRepo<Spptag>
    {
        Task<Spptag> ViewData(long Idspp);
        Task<List<long>> GetIds(long Idspp, long Idtag);
        Task<bool> Update(Spptag param);
    }
}
