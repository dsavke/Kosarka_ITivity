using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kosarka_ITivity_Dejan_Savanovic.Models
{
    public class UtakmicaDetaljiViewModel
    {
        public int UtakmicaID { get; set; }
        public int KoloID { get; set; }
        public DateTime DatumOdigravanja { get; set; }
        public byte PoeniDomaciTim { get; set; }
        public byte PoeniGostujuciTim { get; set; }
        public int BrojGledalaca { get; set; }
        public string Stadion { get; set; }
        public byte PoeniPrvaCetvrtina { get; set; }
        public byte PoeniDrugaCetvrtina { get; set; }
        public byte PoeniTrecaCetvrtina { get; set; }
        public byte PoeniCetvrtaCetvrtina { get; set; }
        public byte PoeniProduzetak { get; set; }
        public int? DomaciTimSlikaID { get; set; }
        public int? GostujuciTimSlikaID { get; set; }
        public string DomaciTimNaziv { get; set; }
        public string GostujuciTimNaziv { get; set; }
        public string DomaciTimTrener { get; set; }
        public string GostujuciTimTrener { get; set; }
        public byte GostiPrvaCetvrtina { get; set; }
        public byte GostiDrugaCetvrtina { get; set; }
        public byte GostiTrecaCetvrtina { get; set; }
        public byte GostiCetvrtaCetvrtina { get; set; }
        public byte GostiProduzetak { get; set; }
        public int DomaciTimID { get; set; }
        public int GostujuciTimID { get; set; }
    }
}