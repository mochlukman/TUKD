﻿using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Tbpsts
    {
        public long Idtbp { get; set; }
        public long Idsts { get; set; }

        public Sts IdstsNavigation { get; set; }
        public Tbp IdtbpNavigation { get; set; }
    }
}
