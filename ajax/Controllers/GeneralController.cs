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
    public class GeneralController : Controller
    {
        private SampleDBContext1 db = new SampleDBContext1();


        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

       




        [HttpGet]
        public ActionResult Login()
        {
            Session.RemoveAll();
            return View();
        }

        [HttpPost]
        [ValidateInput(true)]
        public ActionResult Login(Login credentials)
        {
            int Logid = credentials.Id;
            string Pssd = credentials.Pssd;
            string LoginType = credentials.LoginType;

           

            try
            {
                if (LoginType == "SystemAdmin")
                {
                    var user = (from u in db.Admins
                                where u.AdminId.Equals(Logid) && u.Pswd.Equals(Pssd)
                                select u).FirstOrDefault();


                    if (user.Pswd == Pssd)
                    {
                        Session["Id"] = user.Pswd;
                        Session["LoginType"] = LoginType;
                        Session["Username"] = user.AdminName;
                        return RedirectToAction("Index", "Admin");
                    }

                    ModelState.AddModelError("Error", "Invalid Email or Password");
                    return View();
                }

                else if (LoginType == "Client")
                {
                    var user = (from u in db.Clients
                                where u.ClientID.Equals(Logid) && u.Pswd.Equals(Pssd)
                                select u).FirstOrDefault();

                    if (user.Pswd == Pssd)
                    {
                        Session["Id"] = user.ClientID;
                        Session["LoginType"] = LoginType;
                        Session["Username"] = user.Name;
                        ViewBag.message = "wellcome user";
                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError("Error", "Invalid Email or Password");
                    return View();
                }

                else if (LoginType == "Broker")
                {
                    var user = (from u in db.Brokers
                                where u.BrokerId.Equals(Logid) && u.Pswd.Equals(Pssd)
                                select u).FirstOrDefault();

                    if (user.Pswd == Pssd)
                    {
                        Session["Id"] = user.BrokerId;
                        Session["LoginType"] = LoginType;
                        Session["Username"] = user.BrokerName;
                        return RedirectToAction("Index", "Broker");
                    }

                    ModelState.AddModelError("Error", "Invalid Email or Password");
                    return View();
                }

                else if (LoginType == "owner")
                {
                    var user = (from u in db.Owners
                                where u.OwnerID.Equals(Logid) && u.Pswd.Equals(Pssd)
                                select u).FirstOrDefault();

                    if (user.Pswd == Pssd)
                    {
                        Session["Id"] = user.OwnerID;
                        Session["LoginType"] = LoginType;
                        Session["Username"] = user.Name;
                        return RedirectToAction("Index", "Owner");
                    }

                    ModelState.AddModelError("Error", "Invalid Email or Password");
                    return View();
                }
            }

            catch (System.NullReferenceException)
            {
                ModelState.AddModelError("Error", "Invalid Email or Password");
                return View();
            }

            return RedirectToAction("Index", "Home");
        }


        public ActionResult Logout()
        {

            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Login");
        }

        public ActionResult NewClient()
        {


            return View();
        }
        [HttpPost]
        public ActionResult NewClient(Client cl)
        {





              if (ModelState.IsValid)
            {
                db.Clients.Add(cl);

                db.SaveChanges();

                ViewBag.message = "client added sucessfully";
                return RedirectToAction("Login");
                



            }


          
            return View(cl);
        }



         public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(Login credentials)
        {
            //dbCMSEntities dbo = new dbCMSEntities();

            string Pssd = credentials.Pssd;
            string LoginType = Session["LoginType"].ToString();
            string NewPssd = credentials.NewPssd;
            int uid = int.Parse(Session["Id"].ToString());

            if (LoginType == "SystemAdmin")
            {
                var user = (from u in db.Admins
                            where u.AdminId.Equals(uid) && u.Pswd.Equals(Pssd)
                            select u).FirstOrDefault();

                try
                {
                    if (user.Pswd == Pssd)
                    {
                        user.Pswd = NewPssd;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Error", "Invalid Old Password");
                    return View();
                }
            }

            else if (LoginType == "Clients")
            {
                var user = (from u in db.Clients
                            where u.ClientID.Equals(uid) && u.Pswd.Equals(Pssd)
                            select u).FirstOrDefault();

                try
                {
                    if (user.Pswd == Pssd)
                    {
                        user.Pswd = NewPssd;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                }

                catch (Exception)
                {
                    ModelState.AddModelError("Error", "Invalid Old Password");
                    return View();
                }
            }

            else if (LoginType == "Broker")
            {
                var user = (from u in db.Brokers
                            where u.BrokerId.Equals(uid) && u.Pswd.Equals(Pssd)
                            select u).FirstOrDefault();
                try
                {

                    if (user.Pswd == Pssd)
                    {
                        user.Pswd = NewPssd;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                }

                catch (Exception)
                {
                    ModelState.AddModelError("Error", "Invalid Old Password");
                    return View();
                }
            }

            else if (LoginType == "owner")
            {
                var user = (from u in db.Owners
                            where u.OwnerID.Equals(uid) && u.Pswd.Equals(Pssd)
                            select u).FirstOrDefault();

                try
                {
                    if (user.Pswd == Pssd)
                    {
                        user.Pswd = NewPssd;
                       
                        db.SaveChanges();
                        return RedirectToAction("Index", "Home");
                    }
                }

                catch (Exception)
                {
                    ModelState.AddModelError("Error", "Invalid Old Password");
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }
    





















        // GET: Clients
        //public ActionResult Index()
        //{
        //    return View(db.Clients.ToList());
        //}

        // GET: Clients/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Client client = db.Clients.Find(id);
        //    if (client == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(client);
        //}

        // GET: Clients/Create
        public ActionResult Signup()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup([Bind(Include = "ClientID,Name,Pswd,UserName,Lock")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientID,Name,Pswd,UserName,Lock")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
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
