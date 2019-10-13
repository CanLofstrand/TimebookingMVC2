using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimebookingMVC2.Controllers
{
    public class HomeController : Controller
    {
        public bool IsLoggedIn { get; set; } = false;
        public ActionResult Index()
        {
            var _isLoggedIn = TempData["isloggedin"] as string;
            if (_isLoggedIn == "true")
            {
                IsLoggedIn = true;
            }
            else
                IsLoggedIn = false;

            ViewBag.isloggedin = IsLoggedIn;

            return View();
        }
    }
}