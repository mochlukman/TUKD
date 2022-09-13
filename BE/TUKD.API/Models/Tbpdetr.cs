﻿using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Tbpdetr
    {
        public long Idtbpdetr { get; set; }
        public long Idrek { get; set; }
        public long Idkeg { get; set; }
        public long Idtbp { get; set; }
        public int Idnojetra { get; set; }
        public decimal? Nilai { get; set; }
        public DateTime? Datecreate { get; set; }
        public DateTime? Dateupdate { get; set; }

        public Mkegiatan IdkegNavigation { get; set; }
        public Jtrnlkas IdnojetraNavigation { get; set; }
        public Daftrekening IdrekNavigation { get; set; }
        public Tbp IdtbpNavigation { get; set; }
    }
}
