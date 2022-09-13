using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TUKD.API.Params
{
    public class PanjarGet
    {
        [Required]
        public long Idunit { get; set; }
        [Required]
        public long? Idbend { get; set; }
        [Required]
        public int Idxkode { get; set; }
        [Required]
        public string Kdstatus { get; set; }
    }
    public class PanjarPost
    {
        public long Idpanjar { get; set; }
        [Required]
        public long Idunit { get; set; }
        [Required]
        public string Nopanjar { get; set; }
        [Required]
        public long? Idbend { get; set; }
        public long? Idpeg { get; set; }
        [Required]
        public int Idxkode { get; set; }
        [Required]
        public string Kdstatus { get; set; }
        public bool? Sttunai { get; set; }
        public bool? Stbank { get; set; }
        [Required]
        public DateTime? Tglpanjar { get; set; }
        [Required]
        public string Uraian { get; set; }
        public DateTime? Tglvalid { get; set; }
    }
}
