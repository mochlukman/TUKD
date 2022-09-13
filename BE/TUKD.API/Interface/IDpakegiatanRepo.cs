using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IDpakegiatanRepo : IRepo<Dpakegiatan>
    {
        Task<List<LookupTreeDto>> Tree(long Idunit, string Kdtahap, bool Header, int? Jnskeg);
    }
}
