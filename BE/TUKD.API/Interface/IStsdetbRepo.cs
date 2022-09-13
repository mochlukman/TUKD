﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IStsdetbRepo : IRepo<Stsdetb>
    {
        Task<List<Stsdetb>> ViewDatas(long Idsts);
        Task<Stsdetb> ViewData(long Idstsdetb);
        Task<bool> Update(Stsdetb param);
    }
}
