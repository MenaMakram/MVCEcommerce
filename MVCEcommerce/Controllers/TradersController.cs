using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCEcommerce.Models;

namespace MVCEcommerce.Controllers
{
    [Authorize(Roles = "Tager")]
    public class TradersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Traders
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
           // var products = db.Products.Include(p => p.Category).Include(p => p.user);
            var products = from p in db.Products
                           where p.UserId == id
                           select p;
            return View(products.ToList());
        }

        // GET: Traders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Traders/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Traders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,CategoryID,Price,Quantity,ExpireDate,Description,Images")] Product products)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpPostedFileBase postedFile = Request.Files["file"];
                    postedFile.SaveAs(Server.MapPath("~/images/") + System.IO.Path.GetFileName(postedFile.FileName));
                    products.Images = "" + postedFile.FileName;
                    products.UserId = User.Identity.GetUserId();
                    db.Products.Add(products);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(products);
                }
                
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", products.CategoryID);
           // ViewBag.UserId = new SelectList(db.Users, "Id", "Email", products.UserId);
            return View(products);
        }

        // GET: Traders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", products.CategoryID);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", products.UserId);
            return View(products);
        }

        // POST: Traders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id ,[Bind(Include = "ID,Name,CategoryID,Price,Quantity,ExpireDate,Description,Images")] Product products)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(products).State = EntityState.Modified;
                Product oldP = (from p in db.Products
                                 where p.ID == id
                                 select p).FirstOrDefault();
                oldP.Name = products.Name ;
                oldP.CategoryID = products.CategoryID;
                oldP.Price = products.Price;
                oldP.Quantity = products.Quantity;
                oldP.ExpireDate = products.ExpireDate;
                oldP.Description = products.Description;

                HttpPostedFileBase postedFile = Request.Files["file"];
                if (postedFile.FileName != "")
                {
                    postedFile.SaveAs(Server.MapPath("~/images/") + System.IO.Path.GetFileName(postedFile.FileName));
                    oldP.Images = "" + postedFile.FileName;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", products.CategoryID);
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", products.UserId);
            return View(products);
        }

        // GET: Traders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Traders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product products = db.Products.Find(id);
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
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
