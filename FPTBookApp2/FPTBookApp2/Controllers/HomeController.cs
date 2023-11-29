 using FPTBookApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FPTBookApp2.Controllers
{
    public class HomeController : Controller
    {
    
            private FPTdbEntities db = new FPTdbEntities();
            public ActionResult Index()
            {

                return View(db.products);
            }


        [ChildActionOnly]
        public ActionResult Nav()
        {
            return PartialView("_Nav", db.categories);
        }


        public ActionResult Sortbycat(string id)
        {
            var res = db.products.Where(x => x.CatID == id);
            return View(res.ToList());
        }

        public ActionResult Search(string searchString)
        {
            if (searchString == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                var res = db.products.Where
                    (x => x.ProName.Contains(searchString));
                return View(res.ToList());
            }

        }

        public ActionResult AddtoCart(string id)
        {
            var obj = db.products.Find(id);
            return View(obj);
        }

        List<product> lstOD = new List<product>();
        [HttpPost]
        public ActionResult AddtoCart(string id, int qty)
        {
            List<product> checkList = TempData["cart"] as List<product>;
            product pr = db.products.Find(id);
            pr.ProQty = qty;
            if (TempData["cart"] == null)
            {
                lstOD.Add(pr);
                TempData["cart"] = lstOD;
            }
            else
            {
                List<product> lstOD2 = TempData["cart"] as List<product>;
                if(lstOD2.Find(x => x.ProID == id) != null)
                {
                    product ch = lstOD2.Find(x => x.ProID == id);
                    pr.ProQty = ch.ProQty + qty;
                    
                    lstOD2.Remove(ch);
                }
                lstOD2.Add(pr);
                TempData["cart"] = lstOD2;
            }
            TempData.Keep();
            Session["num"] = TempData["cart"].ToString().Split(',').Count();
            return RedirectToAction("Index");
            
        }

        public ActionResult deleteItem(string id)
        {
            List<product> cart = (List<product>)TempData["cart"];
            for(int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProID.Equals(id))
                {
                    cart.RemoveAt(i);
                }
                
            }
            TempData["cart"] = cart;
            return RedirectToAction("checkout");
        }

        public ActionResult checkout()
        {
            TempData.Keep();
            return View(); 
        }

        public ActionResult Confirm()
        {
            //get User ID
            string uid = Session["accid"].ToString();
            Order objOr = new Order();
            objOr.AccID = uid;
            objOr.Orderdate = DateTime.Now;
            db.Orders.Add(objOr);
            db.SaveChanges();
            //get the cart content
            List<product> cart = (List<product>)TempData["cart"];
            foreach (product pro in (List<product>)TempData["cart"])
            {
                db.OrderDetails.Add(new OrderDetail 
                {
                    ProID = pro.ProID,
                    qty = pro.ProQty, 
                    price = pro.ProPrice,
                    OrderID = objOr.OrderID
                });
            }
            db.SaveChanges();
            TempData["AlertOrder"] = "Order success !!";
            TempData["cart"] = null;
            return RedirectToAction("Index", "Home");
        }

        protected void SetAlert(string text)
        {
            TempData["AlertMessage"] = text;
        }

        public ActionResult manageOrder(Order Order, OrderDetail OrderDetail)
        {
            string us = Session["accid"].ToString();
            var orders = db.Orders
                .Where(o => o.AccID == us);
            return View(orders) ;
        }

        public ActionResult detailOrder(int id)
        {
            
            var lstOr = db.OrderDetails.Where(x => x.OrderID == id);
            
            return View(lstOr);
        }

        public ActionResult orderAdmin()
        {
           return View(db.Orders);
        }

        public ActionResult delOrder(int id)
        {
            Order osa = db.Orders.Where(x=>x.OrderID==id).FirstOrDefault();
            db.Orders.Remove(osa);
            db.SaveChanges();
            return RedirectToAction("orderAdmin");
        }

        
    }
}