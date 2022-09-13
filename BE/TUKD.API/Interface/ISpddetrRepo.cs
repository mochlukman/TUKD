using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface ISpddetrRepo : IRepo<Spddetr>
    {
        Task<decimal?> TotalNilaiSpd(long Idspd);
        Task<List<long>> GetIdReks(long Idspd, long Idkeg);
        Task<List<SpddetrViewTreeRoot>> TreetableFromSubkegiatan(long Idspd);
        Task<bool> UpdateNilai(long Idssddetr, decimal? Nilai, DateTime? Dateupdate);
    }
}
