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

        public ActionResult Edit(KoloViewModel kolo)
        {

            int koloID = kolo.KoloID;

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
                new UtakmicaPrikazViewModel()
                {
                    KoloID = u.KoloID,
                    DomaciTimNaziv = u.DomaciTimPokazatelj.Naziv,
                    GostujuciTimNaziv = u.GostujuciTimPokazatelj.Naziv,
                    DatumOdigravanja = u.DatumOdigravanja,
                    DomaciTimSlikaID = u.DomaciTimPokazatelj.SlikaID,
                    GostujuciTimSlikaID = u.GostujuciTimPokazatelj.SlikaID,
                    UtakmicaID = u.UtakmicaID,
                    PoeniDomaciTim = u.PoeniDomaciTim,
                    PoeniGostujuciTim = u.PoeniGostujuciTim
                }).ToList();

                return View(utakmice);

            }
        }

        public ActionResult FormaCreateEdit(int utakmicaID, int koloID)
        {
            using (var context = new KosarkaContext())
            {

                ViewBag.Timovi = context.Tims.Where(t => (t.UtakmicasDomaciTim.Where(u => u.KoloID == koloID).Count() == 0) &&
                        (t.UtakmicasGostujuciTim.Where(u => u.KoloID == koloID).Count() == 0))
                        .Select(t => new TimViewModel()
                        {
                            Naziv = t.Naziv,
                            TimID = t.TimID,
                            SlikaID = t.SlikaID,
                            NazivStadiona = t.NazivStadiona,
                            BrojGledalaca = t.BrojGledalaca
                        }).ToList();

                    ViewBag.Timovi.Insert(0, new TimViewModel()
                    {
                        Naziv = "Izaberi tim",
                        TimID = -1,
                        SlikaID = 11,
                        NazivStadiona = "Nema",
                        BrojGledalaca = 0
                    });

                return PartialView("_UtakmicaCreateEditForm",
                    new UtakmicaViewModel()
                    {
                        KoloID = koloID,
                        DomaciTimSlikaID = 11,
                        GostujuciTimSlikaID = 11
                    });
            }
        }

        public ActionResult UtakmicaEditForm(int utakmicaID, int koloID)
        {
            using (var context = new KosarkaContext())
            {

                var utakmica = context.Utakmicas.Find(utakmicaID);

                UtakmicaViewModel utakmicaView = new UtakmicaViewModel()
                {
                    BrojGledalaca = utakmica.BrojGledalaca,
                    BrojMogucihGledalaca = utakmica.DomaciTimPokazatelj.BrojGledalaca,
                    DatumOdigravanja = utakmica.DatumOdigravanja,
                    DomaciTim = utakmica.DomaciTim,
                    DomaciTimSlikaID = utakmica.DomaciTimPokazatelj.SlikaID,
                    GostujuciTim = utakmica.GostujuciTim,
                    GostujuciTimSlikaID = utakmica.GostujuciTimPokazatelj.SlikaID,
                    KoloID = utakmica.KoloID,
                    PoeniCetvrtaCetvrtina = utakmica.PoeniCetvrtaCetvrtina,
                    PoeniDomaciTim = utakmica.PoeniDomaciTim,
                    PoeniDrugaCetvrtina = utakmica.PoeniDrugaCetvrtina,
                    PoeniGostujuciTim = utakmica.PoeniGostujuciTim,
                    PoeniProduzetak = utakmica.PoeniProduzetak,
                    PoeniPrvaCetvrtina = utakmica.PoeniPrvaCetvrtina,
                    PoeniTrecaCetvrtina = utakmica.PoneiTrecaCetvrtina,
                    Stadion = utakmica.DomaciTimPokazatelj.NazivStadiona,
                    UtakmicaID = utakmica.UtakmicaID,
                    DomaciTimNaziv = utakmica.DomaciTimPokazatelj.Naziv,
                    GostujuciTimNaziv = utakmica.GostujuciTimPokazatelj.Naziv
                };

                return PartialView("_UtakmicaEditRezultat", utakmicaView);

            }
        }

        [HttpPost]
        public ActionResult CreateUtakmica(UtakmicaViewModel model)
        {
            using (var context = new KosarkaContext())
            {

                Utakmica utakmica = new Utakmica()
                {
                    BrojGledalaca = model.BrojGledalaca,
                    DatumOdigravanja = model.DatumOdigravanja,
                    DomaciTim = model.DomaciTim,
                    GostujuciTim = model.GostujuciTim,
                    KoloID = model.KoloID,
                    PoeniDomaciTim = model.PoeniDomaciTim,
                    PoeniGostujuciTim = model.PoeniGostujuciTim,
                    PoeniPrvaCetvrtina = model.PoeniPrvaCetvrtina,
                    PoeniDrugaCetvrtina = model.PoeniDrugaCetvrtina,
                    PoneiTrecaCetvrtina = model.PoeniTrecaCetvrtina,
                    PoeniCetvrtaCetvrtina = model.PoeniCetvrtaCetvrtina,
                    PoeniProduzetak = model.PoeniProduzetak
                };

                context.Utakmicas.Add(utakmica);
                context.SaveChanges();

                return RedirectToAction("Edit", new { KoloID = model.KoloID });

            }
        }

        [HttpPost]
        public ActionResult EditUtakmica(UtakmicaViewModel model)
        {
            using (var context = new KosarkaContext())
            {

                Utakmica utakmica = context.Utakmicas.Find(model.UtakmicaID);

                utakmica.BrojGledalaca = model.BrojGledalaca;
                utakmica.DatumOdigravanja = model.DatumOdigravanja;
                utakmica.DomaciTim = model.DomaciTim;
                utakmica.GostujuciTim = model.GostujuciTim;
                utakmica.KoloID = model.KoloID;
                utakmica.PoeniDomaciTim = model.PoeniDomaciTim;
                utakmica.PoeniGostujuciTim = model.PoeniGostujuciTim;
                utakmica.PoeniPrvaCetvrtina = model.PoeniPrvaCetvrtina;
                utakmica.PoeniDrugaCetvrtina = model.PoeniDrugaCetvrtina;
                utakmica.PoneiTrecaCetvrtina = model.PoeniTrecaCetvrtina;
                utakmica.PoeniCetvrtaCetvrtina = model.PoeniCetvrtaCetvrtina;
                utakmica.PoeniProduzetak = model.PoeniProduzetak;

                context.SaveChanges();

                return RedirectToAction("Edit", new { KoloID = model.KoloID });

            }
        }

        [HttpPost]
        public JsonResult Delete(int utakmicaID)
        {
            using(var context = new KosarkaContext())
            {

                context.UcinakIgracas.RemoveRange(context.UcinakIgracas.Where(u => u.UtakmicaID == utakmicaID));

                context.Utakmicas.Remove(context.Utakmicas.Find(utakmicaID));

                context.SaveChanges();

                return Json(new { Success = true });
            }
        }

        public ActionResult GetRaspored(string pocetniDatum, string krajDatum, string id)
        {

            int koloID = Convert.ToInt32(id);
            DateTime pocetniDateTime = DateTime.Parse(pocetniDatum);

            using (var context = new KosarkaContext())
            {
                var utakmice = context.Koloes.Find(koloID).Utakmicas.Select(u =>
                new UtakmicaViewModel()
                {
                    KoloID = u.KoloID,
                    DomaciTimNaziv = u.DomaciTimPokazatelj.Naziv,
                    GostujuciTimNaziv = u.GostujuciTimPokazatelj.Naziv,
                    DatumOdigravanja = u.DatumOdigravanja,
                    DomaciTimSlikaID = u.DomaciTimPokazatelj.SlikaID,
                    GostujuciTimSlikaID = u.GostujuciTimPokazatelj.SlikaID,
                    UtakmicaID = u.UtakmicaID,
                    PoeniDomaciTim = u.PoeniDomaciTim,
                    PoeniGostujuciTim = u.PoeniGostujuciTim,
                    PoeniPrvaCetvrtina = u.PoeniPrvaCetvrtina,
                    PoeniDrugaCetvrtina = u.PoeniDrugaCetvrtina,
                    PoeniTrecaCetvrtina = u.PoneiTrecaCetvrtina,
                    PoeniCetvrtaCetvrtina = u.PoeniCetvrtaCetvrtina,
                    PoeniProduzetak = u.PoeniProduzetak
                }).ToList();

                if(krajDatum.Length > 0)
                {
                    var novaLista = utakmice.Where(u => provjeriDatum(pocetniDateTime, DateTime.Parse(krajDatum)
                        , u.DatumOdigravanja)).ToList();

                    return PartialView("_RasporedUtakmica", novaLista);
                }
                else
                {
                    var drugaLista = utakmice.Where(u => u.DatumOdigravanja == pocetniDateTime).ToList();

                    return PartialView("_RasporedUtakmica", drugaLista);
                }

            }
        }

        private bool provjeriDatum(DateTime datumPocetak, DateTime datumKraj, DateTime pretrazivaniDatum)
        {

            if (pretrazivaniDatum == datumPocetak || pretrazivaniDatum == datumKraj) return true;

            else if(datumPocetak > datumKraj)
            {
                if (pretrazivaniDatum < datumPocetak && pretrazivaniDatum > datumKraj) return true;
                else return false;
            }
            else
            {
                if (pretrazivaniDatum > datumPocetak && pretrazivaniDatum < datumKraj) return true;
                else return false;
            }
        }

    }
}