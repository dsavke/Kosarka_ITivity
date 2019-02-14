using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kosarka_ITivity_Dejan_Savanovic.Models
{
    public class UtakmicaViewModel
    {
        public int UtakmicaID { get; set; }
        public int KoloID { get; set; }
        public DateTime DatumOdigravanja { get; set; }
        public int DomaciTim { get; set; }
        public int GostujuciTim { get; set; }
        public byte PoeniDomaciTim { get; set; }
        public byte PoeniGostujuciTim { get; set; }
        public int BrojGledalaca { get; set; }
        public string Stadion { get; set; }
        public int BrojMogucihGledalaca { get; set; }
        public int PoeniPrvaCetvrtina { get; set; }
        public int PoeniDrugaCetvrtina { get; set; }
        public int PoeniTrecaCetvrtina { get; set; }
        public int PoeniCetvrtaCetvrtina { get; set; }
        public int PoeniProduzetak { get; set; }
        public int DomaciTimSlikaID { get; set; }
        public int GostujuciTimSlikaID { get; set; }
    }
}