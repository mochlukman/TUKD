using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUKD.API.Models;

namespace TUKD.API.Dto
{
    public class KontrakdetrView
    {
        public long Iddetkontrak { get; set; }
        public long Idkontrak { get; set; }
        public long Idrek { get; set; }
        public Daftrekening Rekening { get; set; }
        public int Idbulan { get; set; }
        public Bulan Bulan { get; set; }
        public long Idjtermorlun { get; set; }
        public decimal? Nilai { get; set; }
        public DateTime? Datecreate { get; set; }
        public DateTime? Dateupdate { get; set; }

        public Jtermorlun IdjtermorlunNavigation { get; set; }
    }
}
