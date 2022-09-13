using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Setuprlo
    {
        public long Idsetuprlo { get; set; }
        public long Idrek { get; set; }
        public long Idreklra { get; set; }

        public Daftrekening IdrekNavigation { get; set; }
    }
}
