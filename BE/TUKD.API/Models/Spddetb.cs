﻿using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Spddetb
    {
        public long Idspddetb { get; set; }
        public long Idspd { get; set; }
        public long Idrek { get; set; }
        public decimal? Nilai { get; set; }
        public DateTime? Datecreate { get; set; }
        public DateTime? Dateupdate { get; set; }

        public Daftrekening IdrekNavigation { get; set; }
        public Spd IdspdNavigation { get; set; }
    }
}
