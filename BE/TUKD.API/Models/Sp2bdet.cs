using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Sp2bdet
    {
        public long Idsp2bdet { get; set; }
        public string Nosp2b { get; set; }
        public string Nosp3b { get; set; }
        public decimal? Nilai { get; set; }
        public DateTime? Datecreate { get; set; }
        public DateTime? Dateupdate { get; set; }
    }
}
