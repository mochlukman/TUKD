using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TUKD.API.Params
{
    public class DaftbankPost
    {
        [Required]
        public string Kdbank { get; set; }
        [Required]
        public string Akbank { get; set; }
        [Required]
        public string Alamat { get; set; }
        [Required]
        public string Telepon { get; set; }
        [Required]
        public string Cabang { get; set; }
        public DateTime? Datecreate { get; set; }
        public long Idbank { get; set; }
    }
}
