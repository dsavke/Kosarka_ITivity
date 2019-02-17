using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kosarka_ITivity_Dejan_Savanovic.Models
{
    public class RangTimovaViewModel
    {
        public int? SlikaID { get; set; }
        public string Naziv { get; set; }
        public int? BrojPobjeda { get; set; }
        public int? BrojPoraza { get; set; }
        public int? KosRazlika { get; set; }
        public int? Bodovi { get; set; }

    }
}