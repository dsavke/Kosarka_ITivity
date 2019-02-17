using Kosarka_ITivity_Dejan_Savanovic.DBModels;
using Kosarka_ITivity_Dejan_Savanovic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kosarka_ITivity_Dejan_Savanovic.Controllers
{
    public class IgracController : Controller
    {
        // GET: Igrac
        public ActionResult Index()
        {
            using (var context = new KosarkaContext())
            {

                var igraci = context.Igracs.Select(i =>
                new IgracViewModel()
                {
                    IgracID = i.IgracID,
                    Ime = i.Ime,
                    Prezime = i.Prezime,
                    DatumRodjenja = i.DatumRodjenja,
                    GradID = i.GradID,
                    PozicijaID = i.PozicijaID,
                    SlikaID = i.SlikaID,
                    TimID = i.TimID,
                    BrojDresa = i.BrojDresa,
                    Drzava = i.Drzava,
                    GradNaziv = i.Grad.Naziv,
                    PozicijaSkraceno = i.Pozicija.SkraceniNaziv,
                    TimNaziv = i.Tim.Naziv
                }).ToList();
                return View(igraci);

            }
        }

        public ActionResult Create()
        {
            using (var context = new KosarkaContext())
            {

                ViewBag.Gradovi = context.Grads.Select(g =>
                new SelectListItem()
                {
                    Text = g.Naziv,
                    Value = "" + g.GradID
                }).ToList();

                ViewBag.Timovi = context.Tims.Select(t =>
                new SelectListItem()
                {
                    Text = t.Naziv,
                    Value = "" + t.TimID
                }).ToList();

                ViewBag.Pozicije = context.Pozicijas.Select(p =>
                new SelectListItem()
                {
                    Text = p.Naziv,
                    Value = "" + p.PozicijaID
                }).ToList();

                return PartialView("_IgracModal", new IgracViewModel() { SlikaID = 11 });
            }
        }

        [HttpPost]
        public ActionResult Create(string ime, string prezime, int pozicijaID, string drzava, int slikaID, string slika, DateTime datumRodjenja, int timID, int gradID, byte brojDresa)
        {

            /*try
            {*/
                using (var context = new KosarkaContext())
                {
                    Slika s = new Slika() { SlikaID = Convert.ToInt32(slikaID) };

                    if (s.SlikaID == -1)
                    {
                        var novaSlika = slika.Substring(22);

                        byte[] bytes = Convert.FromBase64String(novaSlika);

                        s.Slika1 = bytes;

                        context.Slikas.Add(s);
                        context.SaveChanges();

                    }

                    Igrac igrac = new Igrac()
                    {
                        Ime = ime,
                        Prezime = prezime,
                        Drzava = drzava,
                        PozicijaID = pozicijaID,
                        DatumRodjenja = datumRodjenja,
                        TimID = timID,
                        SlikaID = s.SlikaID,
                        GradID = gradID,
                        BrojDresa = brojDresa
                    };

                    context.Igracs.Add(igrac);
                    context.SaveChanges();

                    return new JsonResult() { Data = new { Success = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
                }
            //}
            /*catch(Exception ex)
            {
                return new JsonResult() { Data = new { Success = false, Message = ex.Message}, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }*/
            
        }

        [HttpPost]
        public ActionResult Edit(string ime, string prezime, int pozicijaID, string drzava, int slikaID, string slika, DateTime datumRodjenja, int timID, int gradID, int igracID, byte brojDresa)
        {

            using (var context = new KosarkaContext())
            {
                Slika s = new Slika() { SlikaID = Convert.ToInt32(slikaID) };

                if (s.SlikaID == -1)
                {
                    var novaSlika = slika.Substring(22);

                    byte[] bytes = Convert.FromBase64String(novaSlika);

                    s.Slika1 = bytes;

                    context.Slikas.Add(s);
                    context.SaveChanges();

                }

                Igrac igrac = context.Igracs.Find(igracID);

                igrac.Ime = ime;
                igrac.Prezime = prezime;
                igrac.Drzava = drzava;
                igrac.PozicijaID = pozicijaID;
                igrac.DatumRodjenja = datumRodjenja;
                igrac.TimID = timID;
                igrac.SlikaID = s.SlikaID;
                igrac.GradID = gradID;
                igrac.BrojDresa = brojDresa;

                context.SaveChanges();

                return new JsonResult() { Data = new { Success = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
            //}
            /*catch(Exception ex)
            {
                return new JsonResult() { Data = new { Success = false, Message = ex.Message}, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }*/

        }

        public ActionResult Edit(int id)
        {
            using (var context = new KosarkaContext())
            {

                Igrac igrac = context.Igracs.Find(id);

                IgracViewModel igracViewModel = new IgracViewModel()
                {
                    IgracID = igrac.IgracID,
                    GradID = igrac.GradID,
                    PozicijaID = igrac.PozicijaID,
                    Ime = igrac.Ime,
                    Prezime = igrac.Prezime,
                    Drzava = igrac.Drzava,
                    SlikaID = igrac.SlikaID,
                    DatumRodjenja = igrac.DatumRodjenja,
                    TimID = igrac.TimID,
                    BrojDresa = igrac.BrojDresa
                };

                ViewBag.Gradovi = context.Grads.Select(g =>
                new SelectListItem()
                {
                    Text = g.Naziv,
                    Value = "" + g.GradID
                }).ToList();

                ViewBag.Timovi = context.Tims.Select(t =>
                new SelectListItem()
                {
                    Text = t.Naziv,
                    Value = "" + t.TimID
                }).ToList();

                ViewBag.Pozicije = context.Pozicijas.Select(p =>
                new SelectListItem()
                {
                    Text = p.Naziv,
                    Value = "" + p.PozicijaID
                }).ToList();

                return PartialView("_IgracModalEdit", igracViewModel);
            }
        }

        public ActionResult Detail(int igracID)
        {
            using(var context = new KosarkaContext())
            {
                var igrac = context.Igracs.Find(igracID);

                var igracViewModel = new IgracDetailViewModel()
                {
                    TimNaziv = igrac.Tim.Naziv,
                    BrojDresa = igrac.BrojDresa,
                    DatumRodjenja = igrac.DatumRodjenja,
                    Drzava = igrac.Drzava,
                    GradNaziv = igrac.Grad.Naziv,
                    IgracID = igrac.IgracID,
                    Ime = igrac.Ime,
                    PozicijaNaziv = igrac.Pozicija.Naziv,
                    PozicijaSkraceno = igrac.Pozicija.SkraceniNaziv,
                    Prezime = igrac.Prezime,
                    SlikaID = igrac.SlikaID
                };

                ViewBag.Utakmice = igrac.UcinakIgracas.Select(u =>
                new UtakmiceIgracaViewModel()
                {
                    Asistencije = u.Asistencije,
                    DatumOdigravanja = u.Utakmica.DatumOdigravanja,
                    BrojMinuta = u.BrojMinuta,
                    DomaciTim = u.Utakmica.DomaciTimPokazatelj.Naziv,
                    GostujuciTim = u.Utakmica.GostujuciTimPokazatelj.Naziv,
                    Poeni = u.Poeni,
                    PoeniDomaciTim = u.Utakmica.PoeniDomaciTim,
                    PoeniGostujuciTim = u.Utakmica.PoeniGostujuciTim,
                    Skokovi = u.Skokovi,
                    DomaciTimID = u.Utakmica.DomaciTimPokazatelj.TimID,
                    GostujuciTimID = u.Utakmica.GostujuciTimPokazatelj.TimID,
                    NjegovTimID = u.Igrac.TimID
                }).ToList();

                return View(igracViewModel);

            }
        }

    }
}