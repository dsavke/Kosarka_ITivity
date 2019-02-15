using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kosarka_ITivity_Dejan_Savanovic.Models
{
    public class UtakmicaPrikazViewModel
    {
        public int UtakmicaID { get; set; }
        public int KoloID { get; set; }
        public string DomaciTimNaziv { get; set; }
        public string GostujuciTimNaziv { get; set; }
        public DateTime DatumOdigravanja { get; set; }
        public byte PoeniDomaciTim { get; set; }
        public byte PoeniGostujuciTim { get; set; }
        public int? DomaciTimSlikaID { get; set; }
        public int? GostujuciTimSlikaID { get; set; }
    }
}