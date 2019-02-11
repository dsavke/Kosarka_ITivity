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

                var tabela = context.Tims.Select(t => new RangTimovaViewModel()
                {
                    SlikaID = (t.SlikaID == null?1:t.SlikaID),

                    Naziv = t.Naziv,

                    BrojPobjeda = 0 + t.UtakmicasDomaciTim
                    .Where(u => u.PoeniDomaciTim > u.PoeniGostujuciTim)
                    .ToList().Count + t.UtakmicasGostujuciTim
                    .Where(u => u.PoeniGostujuciTim > u.PoeniDomaciTim).ToList().Count,

                    BrojPoraza = 0 + t.UtakmicasDomaciTim
                    .Where(u => u.PoeniDomaciTim < u.PoeniGostujuciTim)
                    .ToList().Count + t.UtakmicasGostujuciTim
                    .Where(u => u.PoeniGostujuciTim < u.PoeniDomaciTim).ToList().Count,

                    KosRazlika = 0 + (t.UtakmicasDomaciTim.Sum(u => u.PoeniDomaciTim) +
                    t.UtakmicasGostujuciTim.Sum(u => u.PoeniGostujuciTim)) - 
                    (t.UtakmicasDomaciTim.Sum(u => u.PoeniGostujuciTim) +
                    t.UtakmicasGostujuciTim.Sum(u => u.PoeniDomaciTim)),

                    Bodovi = 0 + (t.UtakmicasDomaciTim.
                    Where(u => u.PoeniDomaciTim > u.PoeniGostujuciTim)
                    .ToList().Count * 2) + (t.UtakmicasGostujuciTim
                    .Where(u => u.PoeniGostujuciTim > u.PoeniDomaciTim)
                    .ToList().Count * 2) + ((t.UtakmicasDomaciTim
                    .Where(u => u.PoeniDomaciTim < u.PoeniGostujuciTim)
                    .ToList().Count * 1) + (t.UtakmicasGostujuciTim
                    .Where(u => u.PoeniGostujuciTim < u.PoeniDomaciTim).ToList().Count * 1))

                }).ToList();

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

    }
}