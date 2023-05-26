using red.Filter;
using red.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace red.Controllers
{
    public class HomeController : Controller
    {
        DBEmployeeEntities context = new DBEmployeeEntities();


        [HttpGet]
        [CheckLoginFilter]
        public ActionResult HomePage()
        {

            return View();
        }
        [HttpGet]

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult DoLogin(TBLUser u)
        {
            TBLUser checkUser = context.TBLUsers.Where(a => a.UserName == u.UserName && a.Password == u.Password).FirstOrDefault();
            if (checkUser != null)
            {
                Session["CurrentUser"] = checkUser.Id_PK;
                return RedirectToAction("HomePage");

            }
            else
            {
                ViewData["errorMessage"] = "Enter right credencials";
                return View("Login");
            }

        }
    }
}