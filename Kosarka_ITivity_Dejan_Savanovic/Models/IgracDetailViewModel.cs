using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kosarka_ITivity_Dejan_Savanovic.Models
{
    public class IgracDetailViewModel
    {
        public int IgracID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Drzava { get; set; }
        public string GradNaziv { get; set; }
        public string TimNaziv { get; set; }
        public string PozicijaNaziv { get; set; }
        public string PozicijaSkraceno { get; set; }
        public int? SlikaID { get; set; }
        public byte BrojDresa { get; set; }
    }
}