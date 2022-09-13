using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface ISppdetrRepo : IRepo<Sppdetr>
    {
        Task<decimal?> TotalNilaiSpp(List<long> Idspp);
        Task<bool> Update(Sppdetr param);
        Task<bool> UpdateNilai(long Idsspdetr, decimal? Nilai, DateTime? Dateupdate, string Updateby);
        Task<List<SppdetrViewTreeRoot>> TreetableFromSubkegiatan(long Idspp, decimal? TotalSpp, decimal? TotalSpd);
    }
}
