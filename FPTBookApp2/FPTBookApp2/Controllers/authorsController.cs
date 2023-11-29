using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using FPTBookApp2.Models;

namespace FPTBookApp2.Controllers
{
    public class authorsController : Controller
    {
        private FPTdbEntities db = new FPTdbEntities();
        public ActionResult Index()
        {
            return View(db.authors.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(author author)
        {
            if (author.ImageFile != null)
            {
                string path = Path.Combine(Server.MapPath("~/Image/"),
                Path.GetFileName(author.ImageFile.FileName));
                author.ImageFile.SaveAs(path);
                author.auImage = Path.GetFileName(author.ImageFile.FileName);
            }
            db.authors.Add(author);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: authors/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            author author = db.authors.Find(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(author author)
        {
            try
            {
                author old = db.authors.Find(author.auID);
                if (author.ImageFile != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Image/"), 
                    Path.GetFileName(author.ImageFile.FileName));
                    author.ImageFile.SaveAs(path);
                    author.auImage = Path.GetFileName(author.ImageFile.FileName);
                }
                else
                {

                    author.auImage = old.auImage;
                }
                db.Entry(old).State = EntityState.Detached;
                db.Entry(author).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return RedirectToAction("Index");
            }         
        }


        public ActionResult Delete(string id)
        {
            try
            {
                author obj = db.authors.Find(id);
                db.authors.Remove(obj);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                SetAlert("This author could not be removed, please try again later!!!");
                return RedirectToAction("Index");
            }
            

        }
        protected void SetAlert(string text)
        {
            TempData["AlertMessage"] = text;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
