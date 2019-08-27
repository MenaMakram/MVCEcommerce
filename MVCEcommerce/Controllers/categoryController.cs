using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCEcommerce.Models;

namespace MVCEcommerce.Controllers
{   [Authorize(Roles ="Admin")]
    public class categoryController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: category
        public ActionResult Index()
        {
            ViewBag.Message = "Your contact page.";
            List<Category> cate = context.Categories.ToList();

            return View(cate);
        }

        // GET: category/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: category/Create
        [HttpPost]
        public ActionResult Create(Category cate)
        {
            try
            {
                HttpPostedFileBase postedFile = Request.Files["file"];
                postedFile.SaveAs(Server.MapPath("~/images/") + System.IO.Path.GetFileName(postedFile.FileName));
                cate.Image = "" + postedFile.FileName;

                context.Categories.Add(cate);
                // TODO: Add insert logic here
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(cate);
            }
        }

        // GET: category/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Message = "Your contact page.";
            Category quer = (from i in context.Categories
                             where i.ID == id
                             select i).FirstOrDefault();

            return View(quer);
        }

        // POST: category/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Category cate)
        {
            try
            {

                //Get Old reference from Context
                Category oldDept =
                    context.Categories.FirstOrDefault(d => d.ID == id); ;
                //Updat eData Dept ==>Html
                oldDept.Name = cate.Name;
                HttpPostedFileBase postedFile = Request.Files["file"];

                if (postedFile.FileName != "")
                {
                    postedFile.SaveAs(Server.MapPath("~/Content/image/") + System.IO.Path.GetFileName(postedFile.FileName));
                    oldDept.Image = "image/" + postedFile.FileName;

                }

                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(cate);
            }
        }

        // GET: category/Delete/5
        public ActionResult Delete(int id)
        {
            Category quer = (from i in context.Categories
                             where i.ID == id
                             select i).FirstOrDefault();
            context.Categories.Remove(quer);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

       
    }
}
