using HabboHotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HabboHotel.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Email == "pippo@franco" && model.Password == "franchinoercriminale")
                {
                    Session["AdminLogged"] = "true";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Le credenziali fornite non sono corrette.");
                }
            }
            return View(model); 
        }
        public ActionResult Logout()
        {
            Session["AdminLogged"] = "false";
            return RedirectToAction("Index"); 
        }

    }
}