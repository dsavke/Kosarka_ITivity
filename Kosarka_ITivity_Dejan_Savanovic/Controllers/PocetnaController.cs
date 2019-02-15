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

                    KosRazlika = (kosRazlika(t.UtakmicasDomaciTim.ToList(), 1) +
                    kosRazlika(t.UtakmicasGostujuciTim.ToList(), 2))  -
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

    }
}