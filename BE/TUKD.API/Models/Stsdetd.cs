﻿using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Stsdetd
    {
        public long Idstsdetd { get; set; }
        public long Idsts { get; set; }
        public long Idrek { get; set; }
        public int Idnojetra { get; set; }
        public decimal? Nilai { get; set; }
        public DateTime? Datecreate { get; set; }
        public DateTime? Dateupdate { get; set; }

        public Jtrnlkas IdnojetraNavigation { get; set; }
        public Daftrekening IdrekNavigation { get; set; }
    }
}
