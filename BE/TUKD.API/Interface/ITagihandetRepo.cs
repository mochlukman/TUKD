﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Interface
{
    public interface ITagihandetRepo : IRepo<Tagihandet>
    {
        Task<bool> Update(Tagihandet param);
    }
}