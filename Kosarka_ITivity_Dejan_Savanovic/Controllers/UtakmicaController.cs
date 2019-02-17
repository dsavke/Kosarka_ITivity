using Kosarka_ITivity_Dejan_Savanovic.DBModels;
using Kosarka_ITivity_Dejan_Savanovic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kosarka_ITivity_Dejan_Savanovic.Controllers
{
    public class UtakmicaController : Controller
    {
        // GET: Utakmica
        public ActionResult Index(int utakmicaID)
        {
            using (var context = new KosarkaContext())
            {

                var utakmica = context.Utakmicas.Find(utakmicaID);

                var utakmicaDetaljiViewModel = new UtakmicaDetaljiViewModel()
                {
                    BrojGledalaca = utakmica.BrojGledalaca,
                    DatumOdigravanja = utakmica.DatumOdigravanja,
                    DomaciTimNaziv = utakmica.DomaciTimPokazatelj.Naziv,
                    DomaciTimSlikaID = utakmica.DomaciTimPokazatelj.SlikaID,
                    GostujuciTimNaziv = utakmica.GostujuciTimPokazatelj.Naziv,
                    GostujuciTimSlikaID = utakmica.GostujuciTimPokazatelj.SlikaID,
                    KoloID = utakmica.KoloID,
                    PoeniCetvrtaCetvrtina = utakmica.PoeniCetvrtaCetvrtina,
                    PoeniDomaciTim = utakmica.PoeniDomaciTim,
                    PoeniDrugaCetvrtina = utakmica.PoeniDrugaCetvrtina,
                    PoeniGostujuciTim = utakmica.PoeniGostujuciTim,
                    PoeniProduzetak = utakmica.PoeniProduzetak,
                    PoeniPrvaCetvrtina = utakmica.PoeniPrvaCetvrtina,
                    PoeniTrecaCetvrtina = utakmica.PoneiTrecaCetvrtina,
                    UtakmicaID = utakmica.UtakmicaID,
                    Stadion = utakmica.DomaciTimPokazatelj.NazivStadiona,
                    DomaciTimTrener = utakmica.DomaciTimPokazatelj.Trener,
                    GostujuciTimTrener = utakmica.GostujuciTimPokazatelj.Trener,
                    GostiCetvrtaCetvrtina = utakmica.CetvrtaCetvrtinaGosti,
                    GostiDrugaCetvrtina = utakmica.DrugaCetvrtinaGosti,
                    GostiProduzetak = utakmica.ProduzetakGosti,
                    GostiPrvaCetvrtina = utakmica.PrvaCetvrtinaGosti,
                    GostiTrecaCetvrtina = utakmica.TrecaCetvrtinaGosti,
                    DomaciTimID = utakmica.DomaciTimPokazatelj.TimID,
                    GostujuciTimID = utakmica.GostujuciTimPokazatelj.TimID,
                    Grad = utakmica.DomaciTimPokazatelj.Grad.Naziv
                };

                return View(utakmicaDetaljiViewModel);
            }
        }

        public ActionResult GetUcinakIgraca(int utakmicaID, int timID)
        {
            using(var context = new KosarkaContext())
            {

                var tim = context.Tims.Find(timID);

                var ucinakIgraca = tim.UcinakIgracas.Where(u => u.UtakmicaID == utakmicaID).Select(u =>
                new UcinakIgracaViewModel()
                {
                    ImeIPrezime = u.Igrac.Ime + " " + u.Igrac.Prezime,
                    BrojDresa = u.Igrac.BrojDresa,
                    Asistencije = u.Asistencije,
                    Blokade = u.Blokade,
                    BrojMinuta = u.BrojMinuta,
                    Faulova = u.Faulova,
                    IzgubljeneLopte = u.IzgubljeneLopte,
                    PogodjenihDvojki = u.PogodjenihDvojki,
                    PogodjenihSlobodnihBacanja = u.PogodjenihSlobodnihBacanja,
                    PogodjenihTrojki = u.PogodjenihTrojki,
                    IgracID = u.IgracID,
                    Skokovi = u.Skokovi,
                    TimID = u.TimID,
                    UcinakIgracaID = u.UcinakIgracaID,
                    UkradeneLopte = u.UkradeneLopte,
                    UkupnoDvojki = u.UkupnoDvojki,
                    UkupnoSlobodnihBacanja = u.UkupnoSlobodnihBacanja,
                    UkupnoTrojki = u.UkupnoTrojki,
                    UtakmicaID = u.UtakmicaID,
                    Poeni = u.Poeni,
                    Pozicija = u.Igrac.Pozicija.SkraceniNaziv
                }).OrderBy(i => i.BrojDresa).ToList();

                ViewBag.Trener = tim.Trener;

                return PartialView("_GetUcinakIgraca", ucinakIgraca);
            }
        }

        public ActionResult GetPartialCreateUcinak(int utakmicaID, int timID)
        {
            using(var context = new KosarkaContext())
            {

                var igraci = context.Tims.Where(t => t.TimID == timID)
                    .SelectMany(t => t.Igracs).ToList();

                var igraciNaUtakmici = context.Utakmicas.Find(utakmicaID).UcinakIgracas
                    .Select(i => i.Igrac).ToList();

                ViewBag.IgraciZaUtakmicu = igraci.Except(igraciNaUtakmici)
                    .Select(i => new IgracViewModel()
                    {
                        SlikaID = i.SlikaID,
                        Ime = i.Ime,
                        Prezime = i.Prezime,
                        IgracID = i.IgracID
                    }).ToList();

                ViewBag.IgraciZaUtakmicu.Insert(0, new IgracViewModel()
                {
                    SlikaID = 11,
                    Ime = "Izaberi",
                    Prezime = "igraca",
                    IgracID = -1
                });

                return PartialView("_UcinakCreate", new UcinakIgracaViewModel() { UtakmicaID = utakmicaID, TimID = timID});
            }
        }

        public JsonResult CreateUcinak(int igracID, int utakmicaID, int timID, byte brojMinuta, byte pSB, byte uSB,
            byte pTrojki, byte uTrojki, byte pDvojki, byte uDvojki, byte skokovoi, byte asistencije,
            byte blokade, int poeni, byte faulovi, byte izgubljene, byte ukradene)
        {

            using(var context = new KosarkaContext())
            {
                UcinakIgraca ucinakIgraca = new UcinakIgraca()
                {
                    Asistencije = asistencije,
                    Blokade = blokade,
                    BrojMinuta = brojMinuta,
                    Faulova = faulovi,
                    IgracID = igracID,
                    IzgubljeneLopte = izgubljene,
                    Poeni = poeni,
                    PogodjenihDvojki = pDvojki,
                    PogodjenihSlobodnihBacanja = pSB,
                    PogodjenihTrojki = pTrojki,
                    Skokovi = skokovoi,
                    TimID = timID,
                    UkradeneLopte = ukradene,
                    UkupnoDvojki = uDvojki,
                    UkupnoSlobodnihBacanja = uSB,
                    UkupnoTrojki = uTrojki,
                    UtakmicaID = utakmicaID
                };

                context.UcinakIgracas.Add(ucinakIgraca);
                context.SaveChanges();

                return Json(new { Success = true });

            }

        }

        public ActionResult GetPartialEditUcinak(int ucinakIgracaID)
        {
            using(var context = new KosarkaContext())
            {

                UcinakIgraca u = context.UcinakIgracas.Find(ucinakIgracaID);

                UcinakIgracaViewModel ucinakIgracaViewModel = new UcinakIgracaViewModel()
                {
                    Asistencije = u.Asistencije,
                    Blokade = u.Blokade,
                    BrojMinuta = u.BrojMinuta,
                    Faulova = u.Faulova,
                    IzgubljeneLopte = u.IzgubljeneLopte,
                    PogodjenihDvojki = u.PogodjenihDvojki,
                    PogodjenihSlobodnihBacanja = u.PogodjenihSlobodnihBacanja,
                    PogodjenihTrojki = u.PogodjenihTrojki,
                    IgracID = u.IgracID,
                    Skokovi = u.Skokovi,
                    TimID = u.TimID,
                    UcinakIgracaID = u.UcinakIgracaID,
                    UkradeneLopte = u.UkradeneLopte,
                    UkupnoDvojki = u.UkupnoDvojki,
                    UkupnoSlobodnihBacanja = u.UkupnoSlobodnihBacanja,
                    UkupnoTrojki = u.UkupnoTrojki,
                    UtakmicaID = u.UtakmicaID,
                    Poeni = u.Poeni,
                    SlikaID = u.Igrac.SlikaID
                };

                return PartialView("_UcinakEdit", ucinakIgracaViewModel);

            }
        }

        public ActionResult EditUcinak(int igracID, int utakmicaID, int timID, byte brojMinuta, byte pSB, byte uSB,
            byte pTrojki, byte uTrojki, byte pDvojki, byte uDvojki, byte skokovoi, byte asistencije,
            byte blokade, int poeni, byte faulovi, byte izgubljene, byte ukradene, int ucinakIgracaID)
        {

            using (var context = new KosarkaContext())
            {
                UcinakIgraca ucinakIgraca = context.UcinakIgracas.Find(ucinakIgracaID);

                ucinakIgraca.Asistencije = asistencije;
                ucinakIgraca.Blokade = blokade;
                ucinakIgraca.BrojMinuta = brojMinuta;
                ucinakIgraca.Faulova = faulovi;
                ucinakIgraca.IgracID = igracID;
                ucinakIgraca.IzgubljeneLopte = izgubljene;
                ucinakIgraca.Poeni = poeni;
                ucinakIgraca.PogodjenihDvojki = pDvojki;
                ucinakIgraca.PogodjenihSlobodnihBacanja = pSB;
                ucinakIgraca.PogodjenihTrojki = pTrojki;
                ucinakIgraca.Skokovi = skokovoi;
                ucinakIgraca.TimID = timID;
                ucinakIgraca.UkradeneLopte = ukradene;
                ucinakIgraca.UkupnoDvojki = uDvojki;
                ucinakIgraca.UkupnoSlobodnihBacanja = uSB;
                ucinakIgraca.UkupnoTrojki = uTrojki;
                ucinakIgraca.UtakmicaID = utakmicaID;

                context.SaveChanges();

                return Json(new { Success = true });

            }

        }

        public JsonResult UcinakDelete(int ucinakIgracaID)
        {
            using(var context = new KosarkaContext())
            {
                context.UcinakIgracas.Remove(context.UcinakIgracas.Find(ucinakIgracaID));
                context.SaveChanges();

                return Json(new { Success = true });

            }
        }

    }
}