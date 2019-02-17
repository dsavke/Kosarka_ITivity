using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kosarka_ITivity_Dejan_Savanovic.Models
{
    public class UtakmiceIgracaViewModel
    {
        public DateTime DatumOdigravanja { get; set; }
        public string DomaciTim { get; set; }
        public string GostujuciTim { get; set; }
        public int PoeniDomaciTim { get; set; }
        public int PoeniGostujuciTim { get; set; }
        public int Poeni { get; set; }
        public int Asistencije { get; set; }
        public int Skokovi { get; set; }
        public byte BrojMinuta { get; set; }
        public int DomaciTimID { get; set; }
        public int GostujuciTimID { get; set; }
        public int NjegovTimID { get; set; }
        public int Ukradene { get; set; }
        public int Blokade { get; set; }
    }
}