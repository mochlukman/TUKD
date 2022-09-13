﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;
using TUKD.API.Params;

namespace TUKD.API.Interface
{
    public interface ISp2dNtpnRepo : IRepo<Sp2dntpn>
    {
        Task<PrimengTableResult<Sp2dntpn>> Paging(PrimengTableParam<Sp2dGet> param);
        Task<Sp2dntpn> ViewData(long Idntpn);
        Task<bool> Update(Sp2dntpn param);
    }
}
