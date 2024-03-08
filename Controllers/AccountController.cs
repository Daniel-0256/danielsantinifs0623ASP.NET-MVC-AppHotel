using AppHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppHotel.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            const string FixedEmail = "admin@example.com";
            const string FixedPassword = "1234";

            if (ModelState.IsValid)
            {
                if (model.Email == FixedEmail && model.Password == FixedPassword)
                {
                    Session["UserLoggedIn"] = true;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["UserLoggedIn"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}