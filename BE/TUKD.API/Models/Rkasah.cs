﻿using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Rkasah
    {
        public long Idrkasah { get; set; }
        public long Idunit { get; set; }
        public long Idpeg { get; set; }
        public string Kdtahap { get; set; }
        public string Uraian { get; set; }
        public DateTime? Tglsah { get; set; }
        public string Createdby { get; set; }
        public DateTime? Createddate { get; set; }
        public string Updateby { get; set; }
        public DateTime? Updatetime { get; set; }

        public Pegawai IdpegNavigation { get; set; }
        public Daftunit IdunitNavigation { get; set; }
    }
}
