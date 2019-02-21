using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NayanTraders.Models;

namespace NayanTraders.Controllers
{
    public class SalesDetailController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        // GET: SalesDetail
        public ActionResult Index()
        {
            var saleDetails = db.SaleDetails.Include(s => s.Product).Include(s => s.Sale);
            return View(saleDetails.ToList());
        }

        // GET: SalesDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesDetail salesDetail = db.SaleDetails.Find(id);
            if (salesDetail == null)
            {
                return HttpNotFound();
            }
            return View(salesDetail);
        }

        // GET: SalesDetail/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.SaleId = new SelectList(db.Sales, "Id", "SaleCode");
            return View();
        }

        // POST: SalesDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SaleId,ProductId,Quantity,rate")] SalesDetail salesDetail)
        {
            if (ModelState.IsValid)
            {
                db.SaleDetails.Add(salesDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", salesDetail.ProductId);
            ViewBag.SaleId = new SelectList(db.Sales, "Id", "SaleCode", salesDetail.SaleId);
            return View(salesDetail);
        }

        // GET: SalesDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesDetail salesDetail = db.SaleDetails.Find(id);
            if (salesDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", salesDetail.ProductId);
            ViewBag.SaleId = new SelectList(db.Sales, "Id", "SaleCode", salesDetail.SaleId);
            return View(salesDetail);
        }

        // POST: SalesDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SaleId,ProductId,Quantity,rate")] SalesDetail salesDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", salesDetail.ProductId);
            ViewBag.SaleId = new SelectList(db.Sales, "Id", "SaleCode", salesDetail.SaleId);
            return View(salesDetail);
        }

        // GET: SalesDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesDetail salesDetail = db.SaleDetails.Find(id);
            if (salesDetail == null)
            {
                return HttpNotFound();
            }
            return View(salesDetail);
        }

        // POST: SalesDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesDetail salesDetail = db.SaleDetails.Find(id);
            db.SaleDetails.Remove(salesDetail);
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
