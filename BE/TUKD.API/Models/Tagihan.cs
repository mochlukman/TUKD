using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Tagihan
    {
        public Tagihan()
        {
            Bpk = new HashSet<Bpk>();
            Spptag = new HashSet<Spptag>();
            Tagihandet = new HashSet<Tagihandet>();
        }

        public long Idtagihan { get; set; }
        public long Idunit { get; set; }
        public long Idkeg { get; set; }
        public string Notagihan { get; set; }
        public DateTime Tgltagihan { get; set; }
        public long Idkontrak { get; set; }
        public string Uraiantagihan { get; set; }
        public DateTime? Tglvalid { get; set; }
        public string Kdstatus { get; set; }
        public DateTime? Datecreate { get; set; }
        public DateTime? Dateupdate { get; set; }

        public Kontrak IdkontrakNavigation { get; set; }
        public ICollection<Bpk> Bpk { get; set; }
        public ICollection<Spptag> Spptag { get; set; }
        public ICollection<Tagihandet> Tagihandet { get; set; }
    }
}
