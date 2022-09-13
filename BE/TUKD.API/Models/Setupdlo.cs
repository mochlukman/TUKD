using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Setupdlo
    {
        public long Idsetupdlo { get; set; }
        public long Idrek { get; set; }
        public long Idreklra { get; set; }

        public Daftrekening IdrekNavigation { get; set; }
    }
}
