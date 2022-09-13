using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TUKD.API.Params
{
    public class TagihanPost
    {
        public long Idtagihan { get; set; }
        [Required]
        public long Idunit { get; set; }
        [Required]
        public long Idkeg { get; set; }
        [Required]
        public string Notagihan { get; set; }
        public DateTime Tgltagihan { get; set; }
        [Required]
        public long Idkontrak { get; set; }
        public string Uraiantagihan { get; set; }
        public DateTime? Tglvalid { get; set; }
        public string Kdstatus { get; set; }
        public DateTime? Datecreate { get; set; }
        public DateTime? Dateupdate { get; set; }
    }
    public class TagihandetPost
    {
        public long Idtagihandet { get; set; }
        [Required]
        public long Idtagihan { get; set; }
        [Required]
        public long Idrek { get; set; }
        [Required]
        public decimal? Nilai { get; set; }
        public DateTime? Datecreate { get; set; }
        public DateTime? Dateupdate { get; set; }
    }
}
