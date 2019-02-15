using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kosarka_ITivity_Dejan_Savanovic.Models
{
    public class UcinakIgracaViewModel
    {
        public int UcinakIgracaID { get; set; }
        public int IgracID { get; set; }
        public int UtakmicaID { get; set; }
        public int TimID { get; set; }
        public byte BrojMinuta { get; set; }
        public byte UkupnoSlobodnihBacanja { get; set; }
        public byte PogodjenihSlobodnihBacanja { get; set; }
        public byte UkupnoDvojki { get; set; }
        public byte PogodjenihDvojki { get; set; }
        public byte UkupnoTrojki { get; set; }
        public byte PogodjenihTrojki { get; set; }
        public byte Faulova { get; set; }
        public byte Skokovi { get; set; }
        public byte Asistencije { get; set; }
        public byte Blokade { get; set; }
        public byte IzgubljeneLopte { get; set; }
        public byte UkradeneLopte { get; set; }
        public string ImeIPrezime { get; set; }
        public int BrojDresa { get; set; }
    }
}