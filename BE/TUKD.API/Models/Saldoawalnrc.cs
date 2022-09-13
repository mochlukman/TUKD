﻿using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Saldoawalnrc
    {
        public long Idsaldo { get; set; }
        public long Idunit { get; set; }
        public long? Idrek { get; set; }
        public string Kdpers { get; set; }
        public decimal? Nilai { get; set; }
        public string Stvalid { get; set; }
        public DateTime? Datecreate { get; set; }
        public DateTime? Dateupdate { get; set; }

        public Daftrekening IdrekNavigation { get; set; }
        public Daftunit IdunitNavigation { get; set; }
    }
}
