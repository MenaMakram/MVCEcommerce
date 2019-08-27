using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Claims;
using System.Data.Entity.Infrastructure;
using MVCEcommerce.Models;

namespace MVCEcommerce.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public ActionResult Index()
        {
            DTO_SHOP sto = new DTO_SHOP();

            sto.Categoeris = context.Categories.Take(3).ToList();
            sto.Products = context.Products.Where(ps => ps.Quantity >= 1).Take(6).ToList();

            return View("Index", sto);
        }
        public ActionResult Navbar()
        {
            var cats = context.Categories.ToList();

            return PartialView("Navbar", cats);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Route("Home/addtocart/{id}")]
        public ActionResult addtocart(int id, int quantity)
        {
            var usdd = context.Products.Where(usd => usd.ID == id).FirstOrDefault();

            if (Session["Products"] == null)
            {
                if (quantity <= usdd.Quantity)
                {
                    List<Dto_Cart> us = new List<Dto_Cart>();
                    Dto_Cart dc = new Dto_Cart();
                    dc.user = usdd;
                    dc.Quantity = quantity;
                    us.Add(dc);
                    Session["products"] = us;
                    TempData["QuantityError"] = "";

                }
                else
                {

                    TempData["QuantityError"] = "Quantity that you need is more than available";


                }

            }
            else
            {
                List<Dto_Cart> us = (List<Dto_Cart>)Session["Products"];
                foreach (var item in us)
                {
                    if (item.user.ID == id)
                    {
                        if (usdd.Quantity >= quantity + item.Quantity)
                        {
                            item.Quantity += quantity;
                            TempData["QuantityError"] = "";

                        }
                        else
                        {
                            item.Quantity = usdd.Quantity;
                            TempData["QuantityError"] = "Quantity that you need is more than available";


                        }
                        return RedirectToAction("ShopProduct/" + id);

                    }
                }
                if (quantity <= usdd.Quantity)
                {
                    Dto_Cart dc = new Dto_Cart();
                    dc.user = usdd;
                    dc.Quantity = quantity;
                    us.Add(dc);
                    Session["products"] = us;
                    TempData["QuantityError"] = "";

                }
                else
                {
                    TempData["QuantityError"] = "Quantity that you need is more than available";
                }
                //Dto_Cart dc = new Dto_Cart();
                //dc.user = usdd;
                //dc.Quantity = quantity;
                //us.Add(dc);
                //Session["products"] = us;
            }
            return RedirectToAction("ShopProduct/" + id);
        }
        [Route("Home/search")]
        public ActionResult Search(string Search)
        {

            if (context.Products.Where(ps => ps.Name.Contains(Search)).FirstOrDefault() != null)
            {
                DTO_SHOP dto = new DTO_SHOP();
                var id = context.Products.Where(ps => ps.Name.Contains(Search) && ps.Quantity >= 1).ToList();
                dto.Products = id;
                dto.Categoeris = context.Categories.ToList();
                return View("Shop", dto);
            }
            else
            {
                return RedirectToAction("Shop");
            }
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Route("Home/Shop")]
        public ActionResult Shop()
        {
            DTO_SHOP dto = new DTO_SHOP();
            var usrprd = context.Products.Where(ps => ps.Quantity >= 1).ToList();
            var catg = context.Categories.ToList();
            dto.Products = usrprd;
            dto.Categoeris = catg;

            return View(dto);
        }
        [Route("Home/Shop/{id}")]

        public ActionResult Shop(int id)
        {
            DTO_SHOP dto = new DTO_SHOP();
            var catg = context.Categories.ToList();
            var usrprd = context.Products.Where(c => c.CategoryID == id && c.Quantity >= 1).ToList();
            dto.Products = usrprd;
            dto.Categoeris = catg;
            return View(dto);
        }
        [Route("Home/ShopProduct/{id}")]
        public ActionResult ShopProduct(int id)
        {
            DTO_ShopProducts dto = new DTO_ShopProducts();
            dto.userprd = context.Products.Where(us => us.ID == id).FirstOrDefault();
            dto.userproduct = context.Products.Where(us => us.CategoryID == dto.userprd.CategoryID && us.Quantity >= 1).ToList();
            return View(dto);
        }
        public ActionResult cart()
        {
            ViewBag.Message = "Your Shop page.";

            return View(Session["Products"]);
        }
        [Route("Home/RemoveCart/{id}")]
        public ActionResult RemoveCart(int id)
        {
            List<Dto_Cart> dto = (List<Dto_Cart>)Session["Products"];
            List<Dto_Cart> dto1 = new List<Dto_Cart>();
            foreach (var item in dto)
            {
                if (item.user.ID != id)
                {
                    dto1.Add(item);
                }
            }
            Session["Products"] = dto1;
            return RedirectToAction("cart");
        }
        [Route("Home/AddQCart/{id}")]
        public ActionResult AddQCart(int id, int Quantity)
        {
            List<Dto_Cart> dto = (List<Dto_Cart>)Session["Products"];

            foreach (var item in dto)
            {
                if (item.user.ID == id)
                {
                    if (item.user.Quantity >= Quantity)
                    {
                        item.Quantity = Quantity;
                        TempData["QuantityError"] = "";

                    }
                    else
                    {
                        item.Quantity = item.user.Quantity;
                        TempData["QuantityError"] = "Quantity that you need is more than available";


                    }
                }
            }
            Session["Products"] = dto;
            return RedirectToAction("cart");
        }
        [Authorize]
        public ActionResult Checkout()
        {
            ViewBag.Message = "Your Shop page.";
            Claim displayName = ClaimsPrincipal.Current.FindFirst(ClaimsPrincipal.Current.Identities.First().NameClaimType);
            TempData["Name"] = displayName != null ? displayName.Value : String.Empty;
            return View();
        }
        [Authorize]
        public ActionResult Orders()
        {
            bool flag = false;
            context = new ApplicationDbContext();
            do
            {
                try
                {
                    var prdlist = (List<Dto_Cart>)Session["Products"];
                    if (prdlist != null)
                    {
                        Claim displayName = ClaimsPrincipal.Current.FindFirst(ClaimsPrincipal.Current.Identities.First().NameClaimType);
                        TempData["Name"] = displayName != null ? displayName.Value : String.Empty;
                        var usr = context.Users.Where(us => us.UserName == displayName.Value).FirstOrDefault();

                        Orders ord = new Orders();
                        ord.CustomerId = usr.Id;
                        //ord.Customer = usr;
                        ord.DeliverDate = DateTime.Now.Date.AddDays(3);//DateTime.Parse("" + (DateTime.Now.Date.Day + 3) + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year);
                        ord.OrderDate = DateTime.Now.Date;
                        bool f = false;
                        foreach (var item in prdlist)
                        {
                            var prd = context.Products.FirstOrDefault(prds => prds.ID == item.user.ID);
                            if (item.Quantity > prd.Quantity)
                            {
                                f = true;
                                break;
                            }
                        }
                        if (!f)
                        {
                            decimal price = 0;
                            foreach (var item in prdlist)
                            {
                                //context.Entry(item.user).State = EntityState.Detached;
                                var prds = context.Products.FirstOrDefault(ds => ds.ID == item.user.ID);
                                if (item.Quantity <= prds.Quantity)
                                {
                                    OrderUserProduct ordprd = new OrderUserProduct();
                                    //ordprd.Product = item.user;
                                    ordprd.Product_ID = item.user.ID;
                                    ordprd.Quantity = item.Quantity;
                                    ord.OrderUserProduct.Add(ordprd);
                                    price += item.Quantity * item.user.Price;
                                    var prd = context.Products.Where(pd => pd.ID == item.user.ID).FirstOrDefault();
                                    prd.Quantity -= item.Quantity;
                                    TempData["Error"] = "";

                                }
                                else
                                {
                                    TempData["Error"] = "Quantity Not Available";
                                    return RedirectToAction("Checkout", "Home");
                                }
                            }
                            ord.TotalOrderCash = price;

                            context.Orders.Add(ord);
                            context.SaveChanges();
                            flag = true;

                            Session["Products"] = null;
                            TempData["Error"] = "Order has been made successfully";
                            return RedirectToAction("Checkout", "Home");
                        }
                        else
                        {
                            TempData["Error"] = "Quantity Not Available";
                            return RedirectToAction("Checkout", "Home");
                        }
                    }
                    else
                    {
                        TempData["Error"] = "there is no products added in the cart";
                        return RedirectToAction("Checkout", "Home");

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    context = new ApplicationDbContext();
                    Session["TempError"] = "Qunatity you need is not available";
                    flag = false;

                }
            }
            while (flag == false);
            return RedirectToAction("Index", "Home");

        }
    }
}