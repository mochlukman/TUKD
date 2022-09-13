﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Dto;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface IDparRepo : IRepo<Dpar>
    {
        Task<bool> UpdateNilai(Dpadetr param, decimal? newTotal);
        Task<decimal?> GetNilai(long Iddpar);
        Task<bool> UpdateULT(Dpar param);
        Task<List<DparView>> GetByBpkdetr(long Idunit, long Idkeg, long Idbpk);
    }
}
