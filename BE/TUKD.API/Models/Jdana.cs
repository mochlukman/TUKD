﻿using System;
using System.Collections.Generic;

namespace TUKD.API.Models
{
    public partial class Jdana
    {
        public Jdana()
        {
            Bpkdetrdana = new HashSet<Bpkdetrdana>();
            Dpadanab = new HashSet<Dpadanab>();
            Dpadanad = new HashSet<Dpadanad>();
            Dpadanar = new HashSet<Dpadanar>();
            Kegsbdana = new HashSet<Kegsbdana>();
            Paketrupdet = new HashSet<Paketrupdet>();
            Sp2ddetbdana = new HashSet<Sp2ddetbdana>();
            Sp2ddetrdana = new HashSet<Sp2ddetrdana>();
            Sppdetbdana = new HashSet<Sppdetbdana>();
            Sppdetrdana = new HashSet<Sppdetrdana>();
        }

        public long Idjdana { get; set; }
        public string Kddana { get; set; }
        public string Nmdana { get; set; }
        public string Ket { get; set; }
        public DateTime? Datecreate { get; set; }
        public DateTime? Dateupdate { get; set; }

        public ICollection<Bpkdetrdana> Bpkdetrdana { get; set; }
        public ICollection<Dpadanab> Dpadanab { get; set; }
        public ICollection<Dpadanad> Dpadanad { get; set; }
        public ICollection<Dpadanar> Dpadanar { get; set; }
        public ICollection<Kegsbdana> Kegsbdana { get; set; }
        public ICollection<Paketrupdet> Paketrupdet { get; set; }
        public ICollection<Sp2ddetbdana> Sp2ddetbdana { get; set; }
        public ICollection<Sp2ddetrdana> Sp2ddetrdana { get; set; }
        public ICollection<Sppdetbdana> Sppdetbdana { get; set; }
        public ICollection<Sppdetrdana> Sppdetrdana { get; set; }
    }
}
