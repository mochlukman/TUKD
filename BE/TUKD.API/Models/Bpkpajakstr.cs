using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Bpkpajakstr
    {
        public Bpkpajakstr()
        {
            Bpkpajakstrdet = new HashSet<Bpkpajakstrdet>();
        }

        public long Idbpkpajakstr { get; set; }
        public long? Idunit { get; set; }
        public string Nomor { get; set; }
        public DateTime? Tanggal { get; set; }
        public string Uraian { get; set; }
        public string Kdstatus { get; set; }
        public DateTime? Datecreate { get; set; }
        public DateTime? Dateupdate { get; set; }

        public Daftunit IdunitNavigation { get; set; }
        public Stattrs KdstatusNavigation { get; set; }
        public ICollection<Bpkpajakstrdet> Bpkpajakstrdet { get; set; }
    }
}
