using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Sppdetr
    {
        public Sppdetr()
        {
            Sppdetrdana = new HashSet<Sppdetrdana>();
            Sppdetrp = new HashSet<Sppdetrp>();
        }

        public long Idsppdetr { get; set; }
        public long Idrek { get; set; }
        public long Idkeg { get; set; }
        public long Idspp { get; set; }
        public int Idnojetra { get; set; }
        public decimal? Nilai { get; set; }
        public DateTime? Createdate { get; set; }
        public string Createby { get; set; }
        public DateTime? Updatedate { get; set; }
        public string Updateby { get; set; }

        public Jtrnlkas IdnojetraNavigation { get; set; }
        public Daftrekening IdrekNavigation { get; set; }
        public Spp IdsppNavigation { get; set; }
        public ICollection<Sppdetrdana> Sppdetrdana { get; set; }
        public ICollection<Sppdetrp> Sppdetrp { get; set; }
    }
}
