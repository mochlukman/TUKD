using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Setmappfk
    {
        public long Idmappfk { get; set; }
        public long Idreknrc { get; set; }
        public long Idrekpot { get; set; }

        public Daftrekening IdreknrcNavigation { get; set; }
    }
}
