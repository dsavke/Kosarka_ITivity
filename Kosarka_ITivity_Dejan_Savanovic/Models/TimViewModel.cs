using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kosarka_ITivity_Dejan_Savanovic.Models
{
    public class TimViewModel
    {
        public int TimID { get; set; }
        public string Naziv { get; set; }
        public int GradID { get; set; }
        public string NazivStadiona { get; set; }
        public string Trener { get; set; }
        public int? SlikaID { get; set; }
        public byte[] Slika { get; set; }
        public int BrojGledalaca { get; set; }
    }
}