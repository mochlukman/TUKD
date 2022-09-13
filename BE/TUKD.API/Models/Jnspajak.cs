using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Jnspajak
    {
        public Jnspajak()
        {
            Pajak = new HashSet<Pajak>();
        }

        public int Idjnspajak { get; set; }
        public string Nmjnspajak { get; set; }

        public ICollection<Pajak> Pajak { get; set; }
    }
}
