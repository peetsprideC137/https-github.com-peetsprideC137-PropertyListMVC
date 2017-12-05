using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ajax.Models;


namespace ajax.Controllers
{
    public class AdminController : Controller
    {
        private SampleDBContext1 db = new SampleDBContext1();



        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Addbroker()
        {
            return View();
        }



        [HttpPost]
        public ActionResult AddBroker(Broker k)
        {
            if (ModelState.IsValid)
            {
                db.Brokers.Add(k);

                db.SaveChanges();
                ViewBag.message = "Broker added sucessfully";
                return RedirectToAction("Index");



            }



            return View(k);


        }



        public ActionResult Addowner()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Addowner(Owner mr)
        {


            if (ModelState.IsValid)
            {
                db.Owners.Add(mr);

                db.SaveChanges();
                
                return RedirectToAction("addonowner", new { owid = mr.OwnerID });



            }



            return View(mr);


        }



        public ActionResult addonowner(int owid=0)
        {
            OwnerAddOn ui = new OwnerAddOn();
            ui.OwnerId=owid;


            return View(ui);

        }

        [HttpPost]
        public ActionResult addonowner(OwnerAddOn et)
        {
           



            if (ModelState.IsValid)
            {
                db.OwnerAddOns.Add(et);

                db.SaveChanges();

                ViewBag.message = "owner added sucessfully";
                return RedirectToAction("Index");
                



            }



            return View();

        }











    }
}