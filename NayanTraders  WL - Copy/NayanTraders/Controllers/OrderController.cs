using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NayanTraders.Controllers
{
    public class OrderController : Controller
    {
        NayanTraders.Models.DataBaseContext db = new Models.DataBaseContext();
        // GET: Order
        public ActionResult Index()
        {
            var b = db.Orders.ToList().OrderByDescending(t => t.OrderDate);
            return View(b);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Order = db.Orders.Find(id);
            if (Order == null)
            {
                return HttpNotFound();
            }
            return View(Order);
        }

        [HttpPost]
        public ActionResult Edit(NayanTraders.Models.Order order)
        {
            if (ModelState.IsValid)
            {

                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }



        public ActionResult OrderDetail(int? id)
        {
            var Od = db.OrderDetails.Where(t => t.OrderId == id).ToList();
            return View(Od);
        }
    }
}