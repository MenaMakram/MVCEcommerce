using MVCEcommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCEcommerce.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminManageController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: AdminManage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DisplayUsers()
        {
            var q = (from i in context.Users
                     select i).ToList();
            return View(q);
        }
        public ActionResult Delete(string id)
        {
            var del = (from i in context.Users
                      where i.Id == id
                      select i).FirstOrDefault();
            if (del.Roles.First().RoleId == "2")
            {
                foreach (var item in del.Products.ToList())
                {
                    context.OrderUserProducts.RemoveRange(item.orderuserproducts.ToList());
                    context.Orders.RemoveRange(item.user.Orders.ToList());
                    context.Products.Remove(item);
                }
            }
            else if (del.Roles.First().RoleId == "3")
            {
                foreach (var item in del.Orders.ToList())
                {
                    context.OrderUserProducts.RemoveRange(item.OrderUserProduct.ToList());
                    context.Orders.Remove(item);
                }
            }
            context.Users.Remove(del);
            context.SaveChanges();
            return RedirectToAction("DisplayUsers");
        }
        public ActionResult Create()
        {
            return View("../Account/Register");
        }
        public ActionResult Details(string id)
        {
            var data = (from i in context.Users
                        where i.Id == id
                        select i).FirstOrDefault();
            return View(data);
        }
       
        public ActionResult DisplayProducts()
        {
            var q = (from i in context.Products
                     select i).ToList();
            return View(q);
        }
        public ActionResult Deletepro(int id)
        {
            var del = (from i in context.Products
                       where i.ID == id
                       select i).FirstOrDefault();
            //var del2 = (from n in context.Pro
            //            where n.ProductID == id
            //            select n).ToList();
            context.Products.Remove(del);
            //foreach (var item in del2)
            //{
            //    context.Products_Images.Remove(item);
            //}
            context.SaveChanges();
            return RedirectToAction("DisplayProducts");
        }
        public ActionResult Detailspro(int id)
        {
            var data = (from i in context.Products
                        where i.ID == id
                        select i).FirstOrDefault();

            return View(data);
        }
        public ActionResult AdminPanel()
        {
            return View();
        }
    }
}