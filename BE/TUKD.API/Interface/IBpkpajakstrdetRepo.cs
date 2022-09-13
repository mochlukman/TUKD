﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface IBpkpajakstrdetRepo : IRepo<Bpkpajakstrdet>
    {
        Task<List<Bpkpajakstrdet>> ViewDatas(BpkpajakstrdetGet param);
        Task<Bpkpajakstrdet> ViewData(long Idbpkpajakstrdet);
        Task<bool> Update(Bpkpajakstrdet param);
    }
}
