using Kosarka_ITivity_Dejan_Savanovic.DBModels;
using Kosarka_ITivity_Dejan_Savanovic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kosarka_ITivity_Dejan_Savanovic.Controllers
{
    public class PocetnaController : Controller
    {
        // GET: Pocetna
        public ActionResult Index()
        {
            using (var context = new KosarkaContext())
            {

                var tabela = context.Tims.ToList().Select(t => new RangTimovaViewModel()
                {
                    SlikaID = (t.SlikaID == null ? 1 : t.SlikaID),

                    Naziv = t.Naziv,

                    BrojPobjeda = 0 + t.UtakmicasDomaciTim
                    .Where(u => u.PoeniDomaciTim > u.PoeniGostujuciTim)
                    .ToList().Count + t.UtakmicasGostujuciTim
                    .Where(u => u.PoeniGostujuciTim > u.PoeniDomaciTim).ToList().Count,

                    BrojPoraza = 0 + t.UtakmicasDomaciTim
                    .Where(u => u.PoeniDomaciTim < u.PoeniGostujuciTim)
                    .ToList().Count + t.UtakmicasGostujuciTim
                    .Where(u => u.PoeniGostujuciTim < u.PoeniDomaciTim).ToList().Count,

                    KosRazlika = (kosRazlika(t.UtakmicasDomaciTim.ToList(), 1) +
                    kosRazlika(t.UtakmicasGostujuciTim.ToList(), 2)) -
                    (kosRazlika(t.UtakmicasDomaciTim.ToList(), 2) +
                    kosRazlika(t.UtakmicasGostujuciTim.ToList(), 1)),

                    Bodovi = 0 + (t.UtakmicasDomaciTim.
                    Where(u => u.PoeniDomaciTim > u.PoeniGostujuciTim)
                    .ToList().Count * 2) + (t.UtakmicasGostujuciTim
                    .Where(u => u.PoeniGostujuciTim > u.PoeniDomaciTim)
                    .ToList().Count * 2) + ((t.UtakmicasDomaciTim
                    .Where(u => u.PoeniDomaciTim < u.PoeniGostujuciTim)
                    .ToList().Count * 1) + (t.UtakmicasGostujuciTim
                    .Where(u => u.PoeniGostujuciTim < u.PoeniDomaciTim).ToList().Count * 1))

                }).OrderByDescending(t => t.Bodovi).ThenByDescending(t => t.KosRazlika).ToList();

              
                var igraci = context.Igracs.ToList().Where(i => i.UcinakIgracas.Count != 0)
                    .Select(i => new TopListViewModel()
                    {
                        ImeIPrezime = i.Ime + " " + i.Prezime,
                        SlikaID = i.SlikaID,
                        TimNaziv = i.Tim.Naziv,
                        Asistencije = Math.Round(i.UcinakIgracas.Average(u => u.Asistencije), 3),
                        Blokade = Math.Round(i.UcinakIgracas.Average(u => u.Blokade), 3),
                        IgracID = i.IgracID,
                        Poeni = Math.Round(i.UcinakIgracas.Average(u => u.Poeni), 3),
                        Skokovi = Math.Round(i.UcinakIgracas.Average(u => u.Skokovi), 3),
                        Sut2pt = Math.Round(i.UcinakIgracas.ToList().Average(u => (izracunajProsjek((double)u.PogodjenihDvojki, (double)u.UkupnoDvojki))), 3),
                        Sut3pt = Math.Round(i.UcinakIgracas.ToList().Average(u => (izracunajProsjek((double)u.PogodjenihTrojki, (double)u.UkupnoTrojki))), 3),
                        Sutft = Math.Round(i.UcinakIgracas.ToList().Average(u => (izracunajProsjek((double)u.PogodjenihSlobodnihBacanja, (double)u.UkupnoSlobodnihBacanja))), 3),
                        Ukradene = Math.Round(i.UcinakIgracas.Average(u => u.UkradeneLopte), 3)
                    }).ToList();

                ViewBag.Top5Skorera = igraci.OrderByDescending(i => i.Poeni).Take(5).ToList(); 
                ViewBag.Top5Skokova = igraci.OrderByDescending(i => i.Skokovi).Take(5).ToList(); 
                ViewBag.Top5Asistencije = igraci.OrderByDescending(i => i.Asistencije).Take(5).ToList(); 
                ViewBag.Top5Ukradene = igraci.OrderByDescending(i => i.Ukradene).Take(5).ToList(); 
                ViewBag.Top5Blokade = igraci.OrderByDescending(i => i.Blokade).Take(5).ToList(); 
                ViewBag.Top53pt = igraci.OrderByDescending(i => i.Sut3pt).Take(5).ToList(); 
                ViewBag.Top52pt = igraci.OrderByDescending(i => i.Sut2pt).Take(5).ToList(); 
                ViewBag.Top5ft = igraci.OrderByDescending(i => i.Sutft).Take(5).ToList();

                ViewBag.mvpRace = igraci.Select(i =>
                new TopViewModel()
                {
                    SlikaID = i.SlikaID,
                    ImeIPrezime = i.ImeIPrezime,
                    TimNaziv = i.TimNaziv,
                    MVPBodovi = Math.Round(((i.Poeni * 3.00) + (i.Asistencije * 2.00) + (i.Skokovi * 1.00) + (i.Ukradene * 1.50) 
                    + (i.Blokade * 1.50) + (i.Sut3pt * 0.70) + (i.Sut2pt * 0.60) + (i.Sutft * 0.50)), 2)
                }).OrderByDescending(i => i.MVPBodovi).Take(5).ToList();

                return View(tabela);
            }
        }



        public ActionResult GetImage(int id)
        {
            using(var context = new KosarkaContext())
            {
                var image = context.Slikas.Find(id).Slika1;
                return File(image, "image/jpg");
            }
        }

        private int kosRazlika(List<Utakmica> utakmice, int tip)
        {
            if (utakmice.Count == 0) return 0;
            else if (tip == 1) return utakmice.Sum(u => u.PoeniDomaciTim);
            else return utakmice.Sum(u => u.PoeniGostujuciTim);
        }

        private double izracunajProsjek(double val1, double val2)
        {
            if (val1 == 0 || val2 == 0) return 0.0;
            else
            {
                return (val1 / val2) * 100.00;
            }
        }

    }
}