using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TUKD.API.Params
{
    public class LoginParam
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Pwd { get; set; }
        public string kdTahun { get; set; }
    }
}
