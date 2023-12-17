using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HakkimizdaController : Controller
    {
        KurumsalWebDBContext db = new KurumsalWebDBContext();
        // GET: Hakkimizda
        public ActionResult Index()
        {
            var sorgu = db.Hakkimizda.ToList();
            return View(sorgu);
        }

        // GET: Hakkimizda/Edit/5
        public ActionResult Edit(int id) //verileri getiren
        {
            var hakkimizda = db.Hakkimizda.Where(x => x.HakkimizdaId == id).FirstOrDefault();
            return View(hakkimizda);
        }

        // POST: Hakkimizda/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Hakkimizda hakkimizda) //verileri gönderen
        {
            if (ModelState.IsValid)
            {
                var h = db.Hakkimizda.Where(x => x.HakkimizdaId == id).FirstOrDefault();
                h.Aciklama = hakkimizda.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hakkimizda);
        }
    }
}
