using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HizmetController : Controller
    {
        private KurumsalWebDBContext db = new KurumsalWebDBContext();
        // GET: Hizmet
        public ActionResult Index()
        {
            var sorgu = db.Hizmet.ToList();
            return View(sorgu);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Hizmet hizmet, HttpPostedFileBase ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (ResimURL != null)
                {
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string resimname = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(500, 500);
                    img.Save("~/Uploads/Hizmet/" + resimname);

                    hizmet.ResimURL = "/Uploads/Hizmet/" + resimname;
                }
                db.Hizmet.Add(hizmet);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(hizmet);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                ViewBag.Uyari = "Güncellenecek Hizmet Bulunamadı";
            }
            var hizmet = db.Hizmet.Find(id);
            if (hizmet == null)
            {
                return HttpNotFound();
            }
            return View(hizmet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Hizmet hizmet, HttpPostedFileBase ResimURL)
        {
            
            if (ModelState.IsValid)
            {
                var r = db.Hizmet.Find(id);
                if (ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(r.ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(r.ResimURL));
                    }
                    WebImage img = new WebImage(ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(ResimURL.FileName);

                    string filename = Guid.NewGuid().ToString() + imginfo.Extension;
                    img.Resize(300, 200);
                    img.Save("~/Uploads/Kimlik/" + filename);

                    r.ResimURL = "/Uploads/Kimlik/" + filename;
                }
                r.Baslik = hizmet.Baslik;
                r.Aciklama = hizmet.Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var h = db.Hizmet.Find(id);
            if (h == null)
            {
                return HttpNotFound();
            }
            db.Hizmet.Remove(h);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}