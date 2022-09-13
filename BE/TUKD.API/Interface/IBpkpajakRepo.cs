﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IBpkpajakRepo : IRepo<Bpkpajak>
    {
        Task<List<BpkpajakView>> ViewDatas(BpkpajakGet param);
        Task<BpkpajakView> ViewData(long Idbpkpajak);
        Task<bool> Update(Bpkpajak param);
    }
}
