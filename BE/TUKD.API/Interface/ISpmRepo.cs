using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface ISpmRepo : IRepo<Spm>
    {
        Task<string> GenerateNoReg(long Idunit);
        Task<string> GenerateNoReg(long Idunit, long Idbend, int Idxkode, string Kdstatus);
        Task<List<long>> GetIds(long Idunit, int Idxkode, string Kdstatus, long Idspp);
        Task<bool> Update(Spm param);
        Task<List<Spm>> ViewDatas(SpmGet param);
        Task<Spm> ViewData(long Idspm);
        Task<bool> Pengesahan(Spm param);
        Task<bool> Penolakan(Spm param);
        Task<List<DataTracking>> TrackingData(long Idspm);
    }
}
