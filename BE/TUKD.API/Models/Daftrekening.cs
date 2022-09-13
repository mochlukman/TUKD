using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Daftrekening
    {
        public Daftrekening()
        {
            Beritadetr = new HashSet<Beritadetr>();
            Bkbkas = new HashSet<Bkbkas>();
            Bktmemdetb = new HashSet<Bktmemdetb>();
            Bktmemdetd = new HashSet<Bktmemdetd>();
            Bktmemdetn = new HashSet<Bktmemdetn>();
            Bktmemdetr = new HashSet<Bktmemdetr>();
            Bpkdetr = new HashSet<Bpkdetr>();
            Dpab = new HashSet<Dpab>();
            Dpad = new HashSet<Dpad>();
            Dpar = new HashSet<Dpar>();
            Jbend = new HashSet<Jbend>();
            Nrcbend = new HashSet<Nrcbend>();
            Prognosisb = new HashSet<Prognosisb>();
            Prognosisd = new HashSet<Prognosisd>();
            Prognosisr = new HashSet<Prognosisr>();
            Rkab = new HashSet<Rkab>();
            Rkad = new HashSet<Rkad>();
            Rkar = new HashSet<Rkar>();
            Saldoawallo = new HashSet<Saldoawallo>();
            Saldoawallra = new HashSet<Saldoawallra>();
            Saldoawalnrc = new HashSet<Saldoawalnrc>();
            Setdlralo = new HashSet<Setdlralo>();
            Setmappfk = new HashSet<Setmappfk>();
            Setnrcmap = new HashSet<Setnrcmap>();
            Setrlralo = new HashSet<Setrlralo>();
            Setum = new HashSet<Setum>();
            Setupdlo = new HashSet<Setupdlo>();
            Setuprlo = new HashSet<Setuprlo>();
            Skpdet = new HashSet<Skpdet>();
            Sp2ddetb = new HashSet<Sp2ddetb>();
            Sp2ddetd = new HashSet<Sp2ddetd>();
            Sp2ddetr = new HashSet<Sp2ddetr>();
            Spddetb = new HashSet<Spddetb>();
            Spddetr = new HashSet<Spddetr>();
            Spmdetb = new HashSet<Spmdetb>();
            Spmdetd = new HashSet<Spmdetd>();
            Sppdetb = new HashSet<Sppdetb>();
            Sppdetd = new HashSet<Sppdetd>();
            Sppdetr = new HashSet<Sppdetr>();
            Stsdetb = new HashSet<Stsdetb>();
            Stsdetd = new HashSet<Stsdetd>();
            Stsdetr = new HashSet<Stsdetr>();
            Tagihandet = new HashSet<Tagihandet>();
            Tbpdetb = new HashSet<Tbpdetb>();
            Tbpdetd = new HashSet<Tbpdetd>();
            Tbpdetr = new HashSet<Tbpdetr>();
        }

        public long Idrek { get; set; }
        public string Kdper { get; set; }
        public string Nmper { get; set; }
        public int Mtglevel { get; set; }
        public int Kdkhusus { get; set; }
        public int Jnsrek { get; set; }
        public long? Idjnsakun { get; set; }
        public string Type { get; set; }
        public int? Staktif { get; set; }
        public DateTime? Datecreate { get; set; }
        public DateTime? Dateupdate { get; set; }

        public Jnsakun IdjnsakunNavigation { get; set; }
        public Jrek JnsrekNavigation { get; set; }
        public Khususrek KdkhususNavigation { get; set; }
        public ICollection<Beritadetr> Beritadetr { get; set; }
        public ICollection<Bkbkas> Bkbkas { get; set; }
        public ICollection<Bktmemdetb> Bktmemdetb { get; set; }
        public ICollection<Bktmemdetd> Bktmemdetd { get; set; }
        public ICollection<Bktmemdetn> Bktmemdetn { get; set; }
        public ICollection<Bktmemdetr> Bktmemdetr { get; set; }
        public ICollection<Bpkdetr> Bpkdetr { get; set; }
        public ICollection<Dpab> Dpab { get; set; }
        public ICollection<Dpad> Dpad { get; set; }
        public ICollection<Dpar> Dpar { get; set; }
        public ICollection<Jbend> Jbend { get; set; }
        public ICollection<Nrcbend> Nrcbend { get; set; }
        public ICollection<Prognosisb> Prognosisb { get; set; }
        public ICollection<Prognosisd> Prognosisd { get; set; }
        public ICollection<Prognosisr> Prognosisr { get; set; }
        public ICollection<Rkab> Rkab { get; set; }
        public ICollection<Rkad> Rkad { get; set; }
        public ICollection<Rkar> Rkar { get; set; }
        public ICollection<Saldoawallo> Saldoawallo { get; set; }
        public ICollection<Saldoawallra> Saldoawallra { get; set; }
        public ICollection<Saldoawalnrc> Saldoawalnrc { get; set; }
        public ICollection<Setdlralo> Setdlralo { get; set; }
        public ICollection<Setmappfk> Setmappfk { get; set; }
        public ICollection<Setnrcmap> Setnrcmap { get; set; }
        public ICollection<Setrlralo> Setrlralo { get; set; }
        public ICollection<Setum> Setum { get; set; }
        public ICollection<Setupdlo> Setupdlo { get; set; }
        public ICollection<Setuprlo> Setuprlo { get; set; }
        public ICollection<Skpdet> Skpdet { get; set; }
        public ICollection<Sp2ddetb> Sp2ddetb { get; set; }
        public ICollection<Sp2ddetd> Sp2ddetd { get; set; }
        public ICollection<Sp2ddetr> Sp2ddetr { get; set; }
        public ICollection<Spddetb> Spddetb { get; set; }
        public ICollection<Spddetr> Spddetr { get; set; }
        public ICollection<Spmdetb> Spmdetb { get; set; }
        public ICollection<Spmdetd> Spmdetd { get; set; }
        public ICollection<Sppdetb> Sppdetb { get; set; }
        public ICollection<Sppdetd> Sppdetd { get; set; }
        public ICollection<Sppdetr> Sppdetr { get; set; }
        public ICollection<Stsdetb> Stsdetb { get; set; }
        public ICollection<Stsdetd> Stsdetd { get; set; }
        public ICollection<Stsdetr> Stsdetr { get; set; }
        public ICollection<Tagihandet> Tagihandet { get; set; }
        public ICollection<Tbpdetb> Tbpdetb { get; set; }
        public ICollection<Tbpdetd> Tbpdetd { get; set; }
        public ICollection<Tbpdetr> Tbpdetr { get; set; }
    }
}
