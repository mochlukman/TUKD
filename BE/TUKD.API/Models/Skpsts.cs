﻿using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Skpsts
    {
        public long Idsts { get; set; }
        public long Idskp { get; set; }

        public Skp IdskpNavigation { get; set; }
        public Sts IdstsNavigation { get; set; }
    }
}
