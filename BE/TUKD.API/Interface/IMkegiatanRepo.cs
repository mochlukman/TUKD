﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IMkegiatanRepo : IRepo<Mkegiatan>
    {
        Task<List<LookupTreeDto>> TreeByDpa(long Idunit, string kdtahap);
        Task<List<Mkegiatan>> Search(string Keyword);
        Task<List<Mkegiatan>> ViewDatas(MkegiatanGet param);
        Task<Mkegiatan> ViewData(long Idkeg);
        Task<PrimengTableResult<Mkegiatan>> Paging(PrimengTableParam<MkegiatanGet> param);
        Task<List<LookupTree>> TreeByKegunit(long Idunit, string kdtahap, string type);
        Task<bool> Update(Mkegiatan param);
    }
}
