using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Atasbend
    {
        public long Idpa { get; set; }
        public long Idpeg { get; set; }
        public string Createdby { get; set; }
        public DateTime? Createddate { get; set; }
        public string Updateby { get; set; }
        public DateTime? Updatetime { get; set; }

        public Pegawai IdpaNavigation { get; set; }
    }
}
