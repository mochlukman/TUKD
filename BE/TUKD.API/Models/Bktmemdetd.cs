using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Bktmemdetd
    {
        public long Idbmdetd { get; set; }
        public long Idbm { get; set; }
        public long Idrek { get; set; }
        public string Kdpers { get; set; }
        public decimal? Nilai { get; set; }

        public Bktmem IdbmNavigation { get; set; }
        public Daftrekening IdrekNavigation { get; set; }
    }
}
