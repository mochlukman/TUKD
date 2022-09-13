using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Khususrek
    {
        public Khususrek()
        {
            Daftrekening = new HashSet<Daftrekening>();
        }

        public long Idkhususrek { get; set; }
        public int Kdkhusus { get; set; }
        public string Nmkhusus { get; set; }

        public ICollection<Daftrekening> Daftrekening { get; set; }
    }
}
