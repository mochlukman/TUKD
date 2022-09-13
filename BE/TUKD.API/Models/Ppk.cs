using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Ppk
    {
        public long Idppk { get; set; }
        public long Idpeg { get; set; }
        public DateTime? Datecreate { get; set; }
        public DateTime? Dateupdate { get; set; }

        public Pegawai IdpegNavigation { get; set; }
    }
}
