using NayanTraders.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NayanTraders.Controllers
{
    public class ShoppingCartController : Controller
    {
        private DataBaseContext db = new DataBaseContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderNow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(Session["Cart"]==null)
            {
                List<NayanTraders.Models.Cart> Iscart = new List<Models.Cart>
                {
                    new Cart(db.Products.Find(id),1)
                };
                Session["Cart"] = Iscart;
            }
            else
            {
                List<Cart> cart = (List<Cart>)Session["Cart"];
                int check = IsExists(id);
                if (check == -1)
                {
                    cart.Add(new Cart(db.Products.Find(id), 1));
                }
                else
                    cart[check].Quantity++;

                //cart.Add(new Cart(db.Products.Find(id),1));
                Session["Cart"] = cart;
            }
            return View("Index");
        }

        public int IsExists(int? id)
        {
            
            List<Cart> cart = (List<Cart>)Session["Cart"];
            for(int i=0;i<cart.Count();i++)
            {
                if (cart[i].Product.Id == id) return i;
                
                
            }
            return -1;
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int check = IsExists(id);
            List<Cart> cart = (List<Cart>)Session["Cart"];
            cart.RemoveAt(check);

            return View("Index");

        }

        public ActionResult CheckOut()
        {
            return View();
        }

        public ActionResult ProcessOrder()
        {
            List<Cart> Cart = (List<Cart>)Session["Cart"];
            int d = (int)Session["id"];
            var l = db.Users.Where(t => t.UserId == d).FirstOrDefault();
    
            Order order = new Order()
            {
                CustomerName = l.FirstName,
                Email = l.EmailID,
                OrderName = DateTime.Now + "_" + l.FirstName,
                OrderDate = System.DateTime.Now,
                PaymentType="Cash On Delivery",
                Status="Processing"
            };
            db.Orders.Add(order);
            db.SaveChanges();

            foreach (var i in Cart)
            {

                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId=order.OrderId,
                    ProductId=i.Product.Id,
                    Price=i.Product.Price,
                    Quantity=i.Quantity


                };
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
            }

            Session["Cart"] = null;

            return View("OrderSuccess");
        }

    }
}