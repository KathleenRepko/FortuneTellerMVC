using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FortuneTellerMVC.Models;

namespace FortuneTellerMVC.Controllers
{
    public class CustomersController : Controller
    {
        private FortuneTellerDBEntities db = new FortuneTellerDBEntities();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            //int retirementAge;

            if (customer.Age % 2 == 0)
            {
                ViewBag.RetirementAge = 10;
            }
            else
            {
                ViewBag.RetirementAge = 7;
            }

            //Vacation home is based on number of siblings user entered.
            //string vacaHome;
            if (customer.NumberOfSiblings == 0)
            {
                ViewBag.VacationHome = "New York City";
            }
            else if (customer.NumberOfSiblings == 1)
            {
                ViewBag.VacationHome = "Chicago";
            }
            else if (customer.NumberOfSiblings == 2)
            {
                ViewBag.VacationHome = "Omaha";
            }
            else if (customer.NumberOfSiblings == 3)
            {
                ViewBag.VacationHome = "Key West";
            }
            else if (customer.NumberOfSiblings > 3)
            {
                ViewBag.VacationHome = "Sedona";
            }
            //If the user enters anything other than a number greater than or equal to 0, they should get a bad vacation home!
            else
            {
                ViewBag.VacationHome = "Siberia";
            }

            //Mode of transportation is determined by user's favorite color.
            //string modeTransportation;
            switch (customer.FavoriteColor)
            {
                case "red":
                    ViewBag.TransportationMode = "little red Corvette";
                    break;
                case "orange":
                    ViewBag.TransportationMode = "scooter";
                    break;
                case "yellow":
                    ViewBag.TransportationMode = "hoverboard";
                    break;
                case "green":
                    ViewBag.TransportationMode = "Jaguar in British Racing Green";
                    break;
                case "blue":
                    ViewBag.TransportationMode = "hot air balloon";
                    break;
                case "indigo":
                    ViewBag.TransportationMode = "VW bus";
                    break;
                case "violet":
                    ViewBag.TransportationMode = "Prius";
                    break;
                case "help":
                    ViewBag.TransportationMode = "unicycle";
                    break;
                default:
                    ViewBag.TransportationMode = "Ford Pinto";
                    break;
            }

            //User's retirement funds are determined by user's birth month.
            //int cashBank;
            if (customer.BirthMonth >= 1 && customer.BirthMonth <= 4)
            {
                ViewBag.CashInBank = 250000;
            }
            else if (customer.BirthMonth >= 5 && customer.BirthMonth <= 8)
            {
                ViewBag.CashInBank = 500000;
            }
            else if (customer.BirthMonth >= 9 && customer.BirthMonth <= 12)
            {
                ViewBag.CashInBank = 750000;
            }
            else
            {
                ViewBag.CashInBank = 0;
            }

            return View(customer);

        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FavoriteColor,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonth,FavoriteColor,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
