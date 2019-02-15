using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kosarka_ITivity_Dejan_Savanovic.Models
{
    public class IgracViewModel
    {
        public int IgracID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DatumRodjenja { get; set; }
        public string Drzava { get; set; }
        public int GradID { get; set; }
        public int TimID { get; set; }
        public int PozicijaID { get; set; }
        public int? SlikaID { get; set; }
        public byte BrojDresa { get; set; }
    }
}