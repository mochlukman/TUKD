﻿using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Bpkpajakdet
    {
        public long Idbpkpajakdet { get; set; }
        public long Idbpkpajak { get; set; }
        public long Idpajak { get; set; }
        public decimal? Nilai { get; set; }
        public DateTime? Datecreate { get; set; }
        public DateTime? Dateupdate { get; set; }

        public Bpkpajak IdbpkpajakNavigation { get; set; }
        public Pajak IdpajakNavigation { get; set; }
    }
}
