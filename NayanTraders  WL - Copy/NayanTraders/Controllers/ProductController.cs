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
    public class ProductController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        // GET: Product
        public ActionResult Index()
        {
           
            var products = db.Products.Include(p => p.Brands).Include(p => p.Category).Include(p => p.Size).Include(p => p.Unit);
             return View(products.ToList());
        }

        // GET: Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            ViewBag.SizeId = new SelectList(db.Sizes, "id", "Name");
            ViewBag.UnitId = new SelectList(db.unites, "Id", "Name");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Code,CategoryId,SizeId,UnitId,BrandId,Price,Discount,Image,Date")] Product product,HttpPostedFileBase imagefile)
        {
            product.Date = DateTime.Now;
            product.Image = System.IO.Path.GetFileName(imagefile.FileName);

            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                imagefile.SaveAs(Server.MapPath("../Upload/ProductImage/" +product.Id+"_"+product.Image));
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            ViewBag.SizeId = new SelectList(db.Sizes, "id", "Name", product.SizeId);
            ViewBag.UnitId = new SelectList(db.unites, "Id", "Name", product.UnitId);
            return View(product);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            ViewBag.SizeId = new SelectList(db.Sizes, "id", "Name", product.SizeId);
            ViewBag.UnitId = new SelectList(db.unites, "Id", "Name", product.UnitId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Code,CategoryId,SizeId,UnitId,BrandId,Price,Discount,Image,Date")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            ViewBag.SizeId = new SelectList(db.Sizes, "id", "Name", product.SizeId);
            ViewBag.UnitId = new SelectList(db.unites, "Id", "Name", product.UnitId);
            return View(product);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult All(string searchstring,int To=0,int From=0)
        {
            var p = from s in db.Products select s;
            if (searchstring != null || To != 0 || From !=0)
           
            {
                if (searchstring != null)
                {
                    p = db.Products.Where(t => t.Name.Contains(searchstring));
                }
                if (From !=0)
                {

                    p = db.Products.Where(t => t.Price > From);
                }
            }


            return View(p.ToList());
        }
        public ActionResult Category()
        {
            var c = db.Categories.ToList();
            return View(c);
        }


        public ActionResult ProductByCredential(int cId=0)
        {
            if(cId!=0)
            {
                var c = db.Products.Where(t => t.CategoryId == cId);
                return View(c.ToList());
            }
            return View();
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
