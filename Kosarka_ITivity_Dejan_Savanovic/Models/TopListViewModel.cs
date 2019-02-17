using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kosarka_ITivity_Dejan_Savanovic.Models
{
    public class TopListViewModel
    {
        public int IgracID { get; set; }
        public string ImeIPrezime { get; set; }
        public string TimNaziv { get; set; }
        public int? SlikaID { get; set; }
        public double Poeni { get; set; }
        public double Skokovi { get; set; }
        public double Asistencije { get; set; }
        public double Ukradene { get; set; }
        public double Blokade { get; set; }
        public double Sut3pt { get; set; }
        public double Sut2pt { get; set; }
        public double Sutft { get; set; }
    }
}