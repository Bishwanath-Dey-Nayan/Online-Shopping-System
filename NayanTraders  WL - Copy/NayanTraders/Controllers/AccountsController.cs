﻿using System;
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
    [Authorize]
    public class AccountsController : Controller
    {
        private DataBaseContext db = new DataBaseContext();

        // GET: Accounts
        public ActionResult Index()
        {
            var accounts = db.Accounts.Include(a => a.City).Include(a => a.Country).Include(a => a.Gender).Include(a => a.UserType);
            return View(accounts.ToList());
        }

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = db.Accounts.Find(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            return View(accounts);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name");
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name");
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "Id", "Name");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Contact,GenderId,Address,CountryId,CityId,Email,UserTypeId,PassWord,CreateDate")] Accounts accounts)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(accounts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", accounts.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", accounts.CountryId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", accounts.GenderId);
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "Id", "Name", accounts.UserTypeId);
            return View(accounts);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = db.Accounts.Find(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", accounts.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", accounts.CountryId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", accounts.GenderId);
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "Id", "Name", accounts.UserTypeId);
            return View(accounts);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Contact,GenderId,Address,CountryId,CityId,Email,UserTypeId,PassWord,CreateDate")] Accounts accounts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accounts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(db.Cities, "Id", "Name", accounts.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", accounts.CountryId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Name", accounts.GenderId);
            ViewBag.UserTypeId = new SelectList(db.UserTypes, "Id", "Name", accounts.UserTypeId);
            return View(accounts);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Accounts accounts = db.Accounts.Find(id);
            if (accounts == null)
            {
                return HttpNotFound();
            }
            return View(accounts);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Accounts accounts = db.Accounts.Find(id);
            db.Accounts.Remove(accounts);
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
