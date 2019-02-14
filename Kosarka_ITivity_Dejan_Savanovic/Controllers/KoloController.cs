using Kosarka_ITivity_Dejan_Savanovic.DBModels;
using Kosarka_ITivity_Dejan_Savanovic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kosarka_ITivity_Dejan_Savanovic.Controllers
{
    public class KoloController : Controller
    {
        // GET: Kolo
        public ActionResult Index()
        {
            using (var context = new KosarkaContext())
            {
                var kola = context.Koloes.Select(k =>
                new KoloViewModel()
                {
                    KoloID = k.KoloID,
                    BrojKola = k.BrojKola,
                    DatumPocetkaKola = k.DatumPocetkaKola,
                    DatumKrajKola = k.DatumKrajKola
                }).ToList();
                return View(kola);
            }
        }

        public ActionResult GetDate(int koloID)
        {
            using(var context = new KosarkaContext())
            {
                var kolo = context.Koloes.Find(koloID);

                return new JsonResult()
                {
                    Data = new
                    {
                        PocetniDatum = kolo.DatumPocetkaKola.ToString("yyyy-MM-dd"),
                        KrajniDatum = kolo.DatumKrajKola.ToString("yyyy-MM-dd")
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

            }
        }

        public ActionResult Create(string brojKola, DateTime pocetakKola, DateTime krajKola)
        {
            using(var context = new KosarkaContext())
            {

                Kolo kolo = new Kolo()
                {
                    BrojKola = Convert.ToInt32(brojKola),
                    DatumPocetkaKola = pocetakKola,
                    DatumKrajKola = krajKola
                };

                context.Koloes.Add(kolo);
                context.SaveChanges();

                return new JsonResult()
                {
                    Data = new
                    {
                        Success = true
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        public JsonResult GetNextBrojKola()
        {
            using(var context = new KosarkaContext())
            {
                var maxBrojKola = context.Koloes.Max(k => k.BrojKola) + 1;

                return new JsonResult() { Data = new { Brojkola = maxBrojKola }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            }
        }

        public ActionResult Edit(FormCollection formCollection)
        {

            string txtValue = formCollection["KoloID"];

            int koloID = Convert.ToInt32(txtValue);

            using (var context = new KosarkaContext())
            {
                Kolo k = context.Koloes.Find(koloID);
                KoloViewModel koloViewModel = new KoloViewModel()
                {
                    KoloID = k.KoloID,
                    BrojKola = k.BrojKola,
                    DatumPocetkaKola = k.DatumPocetkaKola,
                    DatumKrajKola = k.DatumKrajKola
                };

                return View(koloViewModel);
            }
        }

        public ActionResult GetUtakmice(string id)
        {

            int koloID = Convert.ToInt32(id);

            using (var context = new KosarkaContext())
            {
                var utakmice = context.Koloes.Find(koloID).Utakmicas.Select(u =>
                new UtakmicaViewModel()
                {
                    KoloID = u.KoloID,
                    DatumOdigravanja = u.DatumOdigravanja,
                    DomaciTim = u.DomaciTim,
                    GostujuciTim = u.GostujuciTim,
                    PoeniDomaciTim = u.PoeniDomaciTim,
                    PoeniGostujuciTim = u.PoeniGostujuciTim,
                    UtakmicaID = u.UtakmicaID
                }).ToList();

                return View(utakmice);

            }
        }

        public ActionResult FormaCreateEdit(int utakmicaID, int koloID)
        {
            using (var context = new KosarkaContext())
            {
                if (utakmicaID == -1)
                {

                    ViewBag.Timovi = context.Tims.Where(t => (t.UtakmicasDomaciTim.Where(u => u.KoloID == koloID).Count() == 0) &&
                        (t.UtakmicasGostujuciTim.Where(u => u.KoloID == koloID).Count() == 0))
                        .Select(t => new TimViewModel()
                        {
                            Naziv = t.Naziv,
                            TimID = t.TimID,
                            SlikaID = t.SlikaID,
                            NazivStadiona = t.NazivStadiona
                        }).ToList();

                    ViewBag.Timovi.Insert(0, new TimViewModel()
                    {
                        Naziv = "Izaberi tim",
                        TimID = -1,
                        SlikaID = 11,
                        NazivStadiona = "Nema"
                    });

                    return PartialView("_UtakmicaCreateEditForm", 
                        new UtakmicaViewModel()
                        {
                            KoloID = koloID,
                            DomaciTimSlikaID = 11,
                            GostujuciTimSlikaID = 11
                        });
                }
                else
                {
                    var utakmica = context.Utakmicas.Find(utakmicaID);
                    return PartialView();
                
                }
            }
        }

        [HttpPost]
        public ActionResult CreateUtakmica(UtakmicaViewModel model)
        {
         
            return new JsonResult() { Data = new { Success = true }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}