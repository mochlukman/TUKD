using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Setnrcmap
    {
        public long Idsetnrcmap { get; set; }
        public long Idrekaset { get; set; }
        public long Idrekhutang { get; set; }

        public Daftrekening IdrekasetNavigation { get; set; }
    }
}
