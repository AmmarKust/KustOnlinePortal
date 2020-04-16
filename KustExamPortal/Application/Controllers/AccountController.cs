using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Models;
using System.Web.Security;

namespace Application.Controllers
{
    
    public class AccountController : Controller
    {
       [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        
        public ActionResult Login(Authentication auth)
        {
           using(var context =new KustOnlinePortalEntities())
            {
                var isValid = context.Authentications.Any(x => x.RegistrationNo == auth.RegistrationNo && x.Password == auth.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(auth.RegistrationNo, false);

                    this.Session["UserData"] = isValid;

                    return RedirectToAction("Dashboard","Admin");
                }
                ModelState.AddModelError("", "Invalid User");
            }
            return View();
        }


        

        public ActionResult Logout()
        {
            Session.Clear();
            Session.RemoveAll();

            return RedirectToAction("Login");
        }


        
    }
}