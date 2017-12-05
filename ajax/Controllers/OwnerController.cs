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
    public class OwnerController : Controller
    {
        private SampleDBContext1 db = new SampleDBContext1();
        // GET: Owner
        public ActionResult Index()
        {
            return View();
        }




        [HttpGet]
        public ActionResult addprop()
        {

            PropertyDetail pk = new PropertyDetail();
            pk.OwnerID = Convert.ToInt32(Session["Id"]);
                


            return View(pk);
        }


      






        [HttpPost]
        public ActionResult addprop(PropertyDetail pa)
        {
            if (ModelState.IsValid)
            {
               
                db.PropertyDetails.Add(pa);

                db.SaveChanges();
                return RedirectToAction("upload", new { propid = pa.PropertyID });



            }

            return View(pa);

        }




        public ActionResult showlistpropad()
        {
            int idadmin = Convert.ToInt32(Session["Id"]);

            List<PropertyDetail> model = db.PropertyDetails.ToList();

            var pet = from s in model
                      where s.OwnerID.Equals(idadmin)

                      select s;


            return View(pet);
        }


        public ActionResult showbidsadmin(int idpr = 0)
        {

           

            List<BiddingProperty> model = db.BiddingProperties.ToList();

            var pet = from s in model
                      where s.PropertyId.Equals(idpr)

                      select s;


            return View(pet);
        }


        public ActionResult approvereq(int idbh = 0)
        {







            BiddingProperty rt = db.BiddingProperties.Find(idbh);//this checks only the primary key

            BidApproved hj = new BidApproved();
            hj.BId = rt.BiddingId;
            hj.ClientId = rt.ClientId;
            hj.PropertyId = rt.PropertyId;

            ViewBag.teme = rt.PropertyId;

            db.BidApproveds.Add(hj);
            db.BiddingProperties.Remove(rt);

            db.SaveChanges();
            ViewBag.message = "the record has been approved";

            return RedirectToAction("Index");



              

        }





        public ActionResult rejectreq(int idbh = 0)
        {







            BiddingProperty rt = db.BiddingProperties.Find(idbh);//this checks only the primary key

            BidRejected hj = new BidRejected();
            hj.BidId = rt.BiddingId;
            hj.ClientId = rt.ClientId;
            hj.PropertyId = rt.PropertyId;

            ViewBag.teme = rt.PropertyId;

            db.BidRejecteds.Add(hj);
            db.BiddingProperties.Remove(rt);

            db.SaveChanges();

            ViewBag.message = "the record has been rejected";
            return RedirectToAction("Index");
            





        }







        [HttpGet]
        public ActionResult upload(int propid = 0)
        {
            ViewBag.testid = propid;

            PropertyDetail pd = db.PropertyDetails.Find(propid);
            if (pd == null)
            {
                return HttpNotFound();
            }
            return View(pd);



        }
        [HttpPost]
        public ActionResult upload(HttpPostedFileBase[] files, int propid = 0)
        {
            string filepathtosave;
            PropertyDetail pm = db.PropertyDetails.Find(propid);
            //AddImageViewModel model1;
            try
            {
                /*Lopp for multiple files*/
                foreach (HttpPostedFileBase file in files)
                {
                    /*Geting the file name*/
                    string filename = System.IO.Path.GetFileName(file.FileName);
                    /*Saving the file in server folder*/
                    file.SaveAs(Server.MapPath("~/Content/Images/" + filename));
                    filepathtosave = "~/Content/Images/" + filename;
                    //ViewBag.retpath = filepathtosave;
                    ViewBag.retpath = Url.Content(filepathtosave);
                    /*HERE WILL BE YOUR CODE TO SAVE THE FILE DETAIL IN DATA BASE*/
                    //@Html.EditorFor(m=>m.LastImage=);

                    var imageURL = Url.Content("~/Content/Images/" + filename);
                    ViewData["ImageURL"] = imageURL;


                    pm.Images = imageURL;
                    pm.Views = 1;

                    pm.Images = imageURL;
                    db.SaveChanges();
                    return RedirectToAction("TypeQues", new { proid = pm.PropertyID });







                }
                ViewBag.Message = "file has been uploaded";

            }
            catch
            {
                filepathtosave = null;
                ViewBag.Message = "cannot save the data";
            }
           
            return View();
        }





        [HttpGet]
        public ActionResult TypeQues(int proid = 0)
        {
            PropertyQuestion pqt = new PropertyQuestion();
           

            pqt.PropertyId = proid;

            return View(pqt);
        }
        [HttpPost]
        public ActionResult TypeQues(PropertyQuestion pqt)
        {
            if (ModelState.IsValid)
            {
                db.PropertyQuestions.Add(pqt);
                
                db.SaveChanges();
                return RedirectToAction("index");



            }

            return View(pqt);

        }














    }
}