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
    public class PurchaseDetailController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        // GET: PurchaseDetail
        public ActionResult Index()
        {
            var purchaseDetail = db.PurchaseDetail.Include(p => p.Brand).Include(p => p.Product).Include(p => p.Purchase);
            return View(purchaseDetail.ToList());
        }

        // GET: PurchaseDetail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseDetails purchaseDetails = db.PurchaseDetail.Find(id);
            if (purchaseDetails == null)
            {
                return HttpNotFound();
            }
            return View(purchaseDetails);
        }

        // GET: PurchaseDetail/Create
        public ActionResult Create()
        {
 
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name");
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            ViewBag.PurchaseId = new SelectList(db.Purchases, "Id", "PurchaseCode");
            return View();
        }

        // POST: PurchaseDetail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PurchaseId,ProductId,BrandId,Qty,Rate,Amount")] PurchaseDetails purchaseDetails)
        {
            //purchaseDetails.Amount = (purchaseDetails.Rate * purchaseDetails.Qty).ToString();
            //ViewBag.amount = purchaseDetails.Amount;
            if (ModelState.IsValid)
            {
                db.PurchaseDetail.Add(purchaseDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", purchaseDetails.BrandId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", purchaseDetails.ProductId);
            ViewBag.PurchaseId = new SelectList(db.Purchases, "Id", "PurchaseCode", purchaseDetails.PurchaseId);
            return View(purchaseDetails);
        }

        // GET: PurchaseDetail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseDetails purchaseDetails = db.PurchaseDetail.Find(id);
            if (purchaseDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", purchaseDetails.BrandId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", purchaseDetails.ProductId);
            ViewBag.PurchaseId = new SelectList(db.Purchases, "Id", "PurchaseCode", purchaseDetails.PurchaseId);
            return View(purchaseDetails);
        }

        // POST: PurchaseDetail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PurchaseId,ProductId,BrandId,Qty,Rate,Amount")] PurchaseDetails purchaseDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(purchaseDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", purchaseDetails.BrandId);
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", purchaseDetails.ProductId);
            ViewBag.PurchaseId = new SelectList(db.Purchases, "Id", "PurchaseCode", purchaseDetails.PurchaseId);
            return View(purchaseDetails);
        }

        // GET: PurchaseDetail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PurchaseDetails purchaseDetails = db.PurchaseDetail.Find(id);
            if (purchaseDetails == null)
            {
                return HttpNotFound();
            }
            return View(purchaseDetails);
        }

        // POST: PurchaseDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PurchaseDetails purchaseDetails = db.PurchaseDetail.Find(id);
            db.PurchaseDetail.Remove(purchaseDetails);
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
