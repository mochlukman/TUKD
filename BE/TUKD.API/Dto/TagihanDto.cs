using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Dto
{
    public class TagihanView
    {
        public long Idtagihan { get; set; }
        public long Idunit { get; set; }
        public long Idkeg { get; set; }
        public Mkegiatan Kegiatan { get; set; }
        public string Notagihan { get; set; }
        public DateTime Tgltagihan { get; set; }
        public long Idkontrak { get; set; }
        public string Uraiantagihan { get; set; }
        public DateTime? Tglvalid { get; set; }
        public string Kdstatus { get; set; }
        public DateTime? Datecreate { get; set; }
        public DateTime? Dateupdate { get; set; }

        public Kontrak IdkontrakNavigation { get; set; }
        public Stattrs KdstatusNavigation { get; set; }
    }
    public class TagihandetView
    {
        public long Idtagihandet { get; set; }
        public long Idtagihan { get; set; }
        public long Idrek { get; set; }
        public Daftrekening Rekening { get; set; }
        public decimal? Nilai { get; set; }
        public DateTime? Datecreate { get; set; }
        public DateTime? Dateupdate { get; set; }
    }
}
