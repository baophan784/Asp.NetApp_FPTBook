using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using FPTBookApp2.Models;

namespace FPTBookApp2.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Account
        FPTdbEntities db = new FPTdbEntities();

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(Account acc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    acc.pass = EncodePassword(acc.pass);
                    db.Accounts.Add(acc);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Accounts");
                }
                return View();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }

            public static string EncodePassword(string originalPassword)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            md5 = new MD5CryptoServiceProvider();
            originalBytes = System.Text.ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(encodedBytes);
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(string accid, string pass)
        {
            var ch_pass = EncodePassword(pass);
            var obj = db.Accounts.Where
                (x => x.AccID.Equals(accid) && x.pass.Equals(ch_pass)).FirstOrDefault();
            if (obj != null)
            {
                Session["fullname"] = obj.fullname;
                Session["accid"] = obj.AccID;
                Session["state"] = Convert.ToInt32(obj.state);
                return RedirectToAction("Index", "Home");
            }
            Response.Write("<script>alert('Login failed, please try again');</script>");
            return View();

        }

        public ActionResult updateProfile(string id)
        {
            var us = db.Accounts.Find(id);

            return View(us);
        }

        [HttpPost]
        public ActionResult updateProfile(Account acc)
        {
            try {   
            Account oldAcc = db.Accounts.Find(acc.AccID);
                if (acc.pass != null)
                {
                    acc.pass = EncodePassword(acc.pass);
                }
                else
                {
                    acc.pass = oldAcc.pass;
                }    
            db.Entry(oldAcc).State = EntityState.Detached;
            db.Entry(acc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
        }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                        validationErrors.Entry.Entity.ToString(),
                        validationError.ErrorMessage);
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            } 

        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

       
    }
}
