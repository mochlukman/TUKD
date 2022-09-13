﻿using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Spmdetb
    {
        public long Idspmdetb { get; set; }
        public long Idspm { get; set; }
        public long Idrek { get; set; }
        public int Idnojetra { get; set; }
        public decimal? Nilai { get; set; }
        public DateTime? Createdate { get; set; }
        public string Createby { get; set; }
        public DateTime? Updatedate { get; set; }
        public string Updateby { get; set; }

        public Daftrekening IdrekNavigation { get; set; }
        public Spm IdspmNavigation { get; set; }
    }
}
