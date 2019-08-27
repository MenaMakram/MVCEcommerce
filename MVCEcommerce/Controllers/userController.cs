using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCEcommerce.Models;

namespace MVCEcommerce.Controllers
{
    public class userController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        // GET: user
        public ActionResult Index()
        {
            string id = User.Identity.GetUserId();

            var user = (from c in context.Users
                        where c.Id ==id
                        select c).FirstOrDefault();

            return View(user);
        }

        // GET: user/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: user/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: user/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: user/Edit/5
        public ActionResult Edit()
        {
            string id = User.Identity.GetUserId();
            var user = (from c in context.Users
                        where c.Id == id
                        select c).FirstOrDefault();
            return View(user);
        }

        // POST: user/Edit/5
        [HttpPost]
        public ActionResult Edit( ApplicationUser collection)
        {
            try
            {
                string id = User.Identity.GetUserId();
                var user = (from c in context.Users
                            where c.Id == id
                            select c).FirstOrDefault();
                user.PhoneNumber = collection.PhoneNumber;
                user.Email = collection.Email;
                user.UserName = collection.UserName;

                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: user/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: user/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
