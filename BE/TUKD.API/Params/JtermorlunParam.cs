﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TUKD.API.Params
{
    public class JtermorlunPost
    {
        public long Idjtermorlun { get; set; }
        [Required]
        public string Nmjtermorlun { get; set; }
    }
}
