using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ajax.Models;

namespace ajax.Controllers
{
    public class HomeController : Controller
    {
        SampleDBContext1 db = new SampleDBContext1();
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult viewrecom()
        {
            int ft = Convert.ToInt32(Session["Id"]);
            List<Recfrombrok> yuij = db.Recfrombroks.Where(w => w.CustId == ft && w.propidone != null).ToList();



            return View(yuij);
        }




        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public PartialViewResult All()
        {
            List<PropertyDetail> model = db.PropertyDetails.ToList();
            //List<PropertyDetail> model = db.PropertyDetails.Where();
            var pet = from s in model
                      where s.PropertyType.Equals("pg")
                      orderby s.Views descending
                      select s;




            return PartialView("_Student", pet);
        }

        public PartialViewResult Top3()
        {
            List<PropertyDetail> model = db.PropertyDetails.ToList();
            //List<PropertyDetail> model = db.PropertyDetails.Where();
            var pet = from s in model
                      where s.PropertyType.Equals("house")
                      orderby s.Views descending
                      select s;




            return PartialView("_Student", pet);


            //List<PropertyDetail> model = db.PropertyDetails.OrderByDescending(x => x.PropertyID).Take(3).ToList();
            //return PartialView("_Student", model);
        }

        public PartialViewResult Bottom3()
        {
            List<PropertyDetail> model = db.PropertyDetails.ToList();
            //List<PropertyDetail> model = db.PropertyDetails.Where();
            var pet = from s in model
                      where s.PropertyType.Equals("flat")
                      orderby s.Views descending
                      select s;




            return PartialView("_Student", pet);


            //List<PropertyDetail> model = db.PropertyDetails.OrderBy(x => x.PropertyID).Take(3).ToList();
            //return PartialView("_Student", model);
        }
        public ActionResult locationview(int id = 0)
        {
            SampleDBContext1 dbo = new SampleDBContext1();


            PropertyDetail iy = new PropertyDetail();

            var zzz = (from u in dbo.PropertyDetails
                       where u.PropertyID.Equals(id)
                       select u).FirstOrDefault();


            zzz.Views = zzz.Views + 1;


            dbo.SaveChanges();


            PropertyDetail st = db.PropertyDetails.Find(id);

            return View(st);

        }


        public ActionResult showstatus()
        {
            int iduser = Convert.ToInt32(Session["Id"]);

            List<BidApproved> model = db.BidApproveds.ToList();

            Showstatusview sh = new Showstatusview();



            var pet = from s in model
                      where s.ClientId.Equals(iduser)

                      select s;//this 


            return View(pet);

        }


        public ActionResult showownerdet(int bidid = 6)
        {

            ViewStatusModel dk = new ViewStatusModel();
            dk.tblBidApproved = db.BidApproveds.Find(bidid);
            dk.tblPropertyDetails = db.PropertyDetails.Find(dk.tblBidApproved.PropertyId);
            dk.tblowneraddon = db.OwnerAddOns.Find(dk.tblPropertyDetails.OwnerID);




            return View(dk);
        }




        public ActionResult bookprop(int ido)
        {
            PropertyDetail nh = db.PropertyDetails.Find(ido);
            BiddingProperty hu = new BiddingProperty();

            PropertyQuestion pq = db.PropertyQuestions.Find(ido);


            hu.PropertyId = ido;
            hu.ClientId = Convert.ToInt32(Session["Id"]);



            ViewBag.ques1 = pq.Question1;
            ViewBag.ques2 = pq.Question2;
            ViewBag.ques3 = pq.Question3;
            ViewBag.ques4 = pq.Question4;





            return View(hu);


        }



        [HttpPost]
        public ActionResult bookprop(BiddingProperty bidquest)
        {//here we put it only if the value is greater than the percentage
            //we dont have records yet
            int f = bidquest.PropertyId;


            int count = 0;

            PropertyQuestion pqest = db.PropertyQuestions.Find(f);
            if (pqest.Response1 == bidquest.Response1)
            {
                count++;


            }

            if (pqest.Response2 == bidquest.Response2)
            {
                count++;


            }

            if (pqest.Response3 == bidquest.Response3)
            {
                count++;


            }


            if (pqest.Response4 == bidquest.Response4)
            {
                count++;


            }


            if (ModelState.IsValid)
            {
                if (pqest.Percentage <= ((count / 4) * 100))
                {
                    db.BiddingProperties.Add(bidquest);

                    db.SaveChanges();
                }
                ViewBag.message = "you have succesfully applied for the property";
                return RedirectToAction("Index");
            }

            return View(bidquest);


        }





        public ActionResult feellazy()
        {
            Recfrombrok yu = new Recfrombrok();
            yu.CustId = Convert.ToInt32(Session["Id"]);

            return View(yu);
        }


        [HttpPost]
        public ActionResult feellazy(Recfrombrok ym)
        {
            if (ModelState.IsValid)
            {
                db.Recfrombroks.Add(ym);

                db.SaveChanges();
                return RedirectToAction("index");



            }

            return View(ym);

        }











    }


   }

