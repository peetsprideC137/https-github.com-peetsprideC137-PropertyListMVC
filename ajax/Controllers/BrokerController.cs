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
    public class BrokerController : Controller
    {
        SampleDBContext1 db = new SampleDBContext1();



        // GET: Broker
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GiveRec()
        {

            int iduser = Convert.ToInt32(Session["Id"]);

            List<Recfrombrok> model = db.Recfrombroks.ToList();

            var pet = from s in model
                      where s.BrokId.Equals(iduser)

                      select s;//this 


            return View(pet);


                       
        }
         
        public ActionResult listprop(int idm=0)
        {
          
            Recfrombrok mod = db.Recfrombroks.Find(idm);

            List<PropertyDetail> model = db.PropertyDetails.Where(x=>x.City==mod.location&&x.Rent>=mod.Minamount&&x.Rent<=mod.Maxamount).ToList();

         

           
            return View(model);
        }





        





    }
}