using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface ISppRepo : IRepo<Spp>
    {
        Task<List<Spp>> ViewDatas(SppGet param);
        Task<Spp> ViewData(long Idspp);
        Task<List<long>> GetIds(long Idunit, int Idxkode, string Kdstatus, long Idspd);
        Task<bool> Update(Spp param);
        Task<string> GenerateNoReg(long Idunit);
        Task<bool> Pengesahan(Spp param);
        Task<bool> Penolakan(Spp param);
        Task<List<DataTracking>> TrackingData(long Idspp);
    }
}
