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
                    GostiTrecaCetvrtina = utakmica.TrecaCetvrtinaGosti
                };

                return View(utakmicaDetaljiViewModel);
            }
        }
    }
}