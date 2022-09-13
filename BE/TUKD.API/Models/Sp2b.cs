using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Sp2b
    {
        public long Idsp2b { get; set; }
        public string Nosp2b { get; set; }
        public long? Idunit { get; set; }
        public DateTime? Tglsp2b { get; set; }
        public string Uraian { get; set; }
        public DateTime? Tglvalid { get; set; }
        public DateTime? Datecreate { get; set; }
        public DateTime? Dateupdate { get; set; }
    }
}
