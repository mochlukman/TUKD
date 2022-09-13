using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Webotor
    {
        public long Groupid { get; set; }
        public string Roleid { get; set; }

        public Webgroup Group { get; set; }
        public Webrole Role { get; set; }
    }
}
