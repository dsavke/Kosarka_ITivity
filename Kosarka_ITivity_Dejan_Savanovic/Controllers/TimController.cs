using Kosarka_ITivity_Dejan_Savanovic.DBModels;
using Kosarka_ITivity_Dejan_Savanovic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kosarka_ITivity_Dejan_Savanovic.Controllers
{
    public class TimController : Controller
    {
        // GET: Tim
        public ActionResult Index()
        {
            using (var context = new KosarkaContext())
            {
                var timovi = context.Tims.Select(t =>
                new TimViewModel()
                {
                    TimID = t.TimID,
                    Naziv = t.Naziv,
                    GradID = t.GradID,
                    NazivStadiona = t.NazivStadiona,
                    Trener = t.Trener,
                    SlikaID = t.SlikaID,
                    BrojGledalaca = t.BrojGledalaca
                }).ToList();
                return View(timovi);
            }
        }

        public ActionResult GetGrads()
        {
            using(var context = new KosarkaContext())
            {
                var gradovi = context.Grads.Select(g => new SelectListItem()
                {
                    Text = g.Naziv,
                    Value = "" + g.GradID
                }).ToList();

                return new JsonResult() { Data = gradovi, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
        }

        [HttpPost]
        public ActionResult Create(string slika, string naziv, string trener, string stadionNaziv, int gradID, int brojGledalaca)
        {

            try {

                var novaSlika = slika.Substring(22);

                byte[] bytes = Convert.FromBase64String(novaSlika);

                using (var context = new KosarkaContext())
                {
                    Slika s = new Slika() { Slika1 = bytes };
                    context.Slikas.Add(s);
                    context.SaveChanges();

                    Tim tim = new Tim() { Naziv = naziv, Trener = trener, NazivStadiona = stadionNaziv, GradID = gradID, SlikaID = s.SlikaID, BrojGledalaca = brojGledalaca};

                    context.Tims.Add(tim);
                    context.SaveChanges();

                }

                return new JsonResult() { Data = new { Success = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }

            catch
            {
                return new JsonResult() { Data = new { Success = false, Message = "Greska" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

        public ActionResult Edit(string id)
        {
            using (var context = new KosarkaContext()) {

                int timID = Convert.ToInt32(id);
                var tim = context.Tims.Find(timID);

                var timViewModel = new TimViewModel()
                {
                    SlikaID = tim.SlikaID,
                    Slika = tim.Slika.Slika1,
                    Naziv = tim.Naziv,
                    GradID = tim.GradID,
                    NazivStadiona = tim.NazivStadiona,
                    TimID = tim.TimID,
                    Trener = tim.Trener,
                    BrojGledalaca = tim.BrojGledalaca
                };

                ViewBag.Gradovi = context.Grads.Select(g => new SelectListItem()
                {
                    Text = g.Naziv,
                    Value = "" + g.GradID
                }).ToList();

                return PartialView("_ModalTimTijelo", timViewModel);

            }
        }

        [HttpPost]
        public ActionResult Edit(int slikaID, int timID, string slika, string naziv, string trener, string stadionNaziv, int gradID, int brojGledalaca)
        {

            try
            {

                using (var context = new KosarkaContext())
                {

                    Tim tim = context.Tims.Find(timID);

                    if(tim.SlikaID != slikaID)
                    {

                        var novaSlika = slika.Substring(22);

                        byte[] bytes = Convert.FromBase64String(novaSlika);

                        Slika s = new Slika() { Slika1 = bytes };
                        context.Slikas.Add(s);
                        context.SaveChanges();

                        tim.SlikaID = s.SlikaID;

                    }

                    tim.Naziv = naziv;
                    tim.Trener = trener;
                    tim.NazivStadiona = stadionNaziv;
                    tim.GradID = gradID;
                    tim.BrojGledalaca = brojGledalaca;

                    context.SaveChanges();

                }

                return new JsonResult() { Data = new { Success = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }

            catch
            {
                return new JsonResult() { Data = new { Success = false, Message = "Greska" }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }

    }
}