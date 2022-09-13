using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TUKD.API.Dto
{
    public class SpjLookupForSpp
    {
        public long Idspj { get; set; }
        public string Nospj { get; set; }
        public DateTime? Tglspj { get; set; }
        public DateTime? Tglbuku { get; set; }
        public string Keterangan { get; set; }
    }
}
