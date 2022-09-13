using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Daftreklak
    {
        public long Idrek { get; set; }
        public string Kdper { get; set; }
        public string Nmper { get; set; }
        public int Mtglevel { get; set; }
        public int Kdkhusus { get; set; }
        public int? Idjnslak { get; set; }
        public string Type { get; set; }
        public int? Staktif { get; set; }
        public decimal? Nlakawal { get; set; }
        public DateTime? Datecreate { get; set; }
        public DateTime? Dateupdate { get; set; }

        public Jnslak IdjnslakNavigation { get; set; }
    }
}
