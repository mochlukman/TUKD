using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IPegawaiRepo : IRepo<Pegawai>
    {
        Task<List<long>> Idpegs(long Idunit);
        Task<List<Pegawai>> ViewDatas(PegawaiGet param);
        Task<Pegawai> ViewData(long Idpeg);
        Task<PrimengTableResult<Pegawai>> Paging(PrimengTableParam<PegawaiGet> param);
        Task<bool> Update(Pegawai param);
    }
}
