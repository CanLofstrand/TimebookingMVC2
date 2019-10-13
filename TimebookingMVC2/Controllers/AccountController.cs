using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TimebookingMVC2.Api;
using TimebookingMVC2.Models;
using TimebookingMVC2.Api.Models;
using static TimebookingMVC2.Models.AccountModels;
using TimebookingMVC2.Api;
using TimebookingMVC2.Api.Models;

namespace TimebookingMVC2.Controllers
{
    public class AccountController : Controller
    {
        private ApiCommunication _apiComm;
        public AccountController()
        {
            _apiComm = new ApiCommunication();
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterModel model)  //Returns error 409 (conflict)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!string.IsNullOrEmpty(model.UserName) && !string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Password))
            {
                var api = new ApiCommunication();

                var user = new User
                {
                    UserName = model.UserName,
                    UserEmail = model.Email,
                    UserPassword = model.Password,
                    UserRole = "User"
                };

                var response = api.PostRegister(user);

                if (!string.IsNullOrEmpty(response))
                {
                    ViewBag.IsRegistered = true;
                    return View();
                }
                else
                    ViewBag.IsRegistered = false;
                    return View();
            }
            else
            {
                return View();
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!string.IsNullOrEmpty(model.UserName) && !string.IsNullOrEmpty(model.Password))
            {
                var api = new ApiCommunication();
                var token = string.Empty;

                if (model.UserName == "Admin")
                {
                    var user = new User()
                    {
                        UserName = model.UserName,
                        UserPassword = model.Password,
                        UserRole = "Admin"
                    };

                    token = api.PostToken(user);
                }
                else
                {
                    var user = new User()
                    {
                        UserName = model.UserName,
                        UserPassword = model.Password,
                        UserRole = "User"
                    };

                    token = api.PostToken(user);
                }

                if (!string.IsNullOrEmpty(token))
                {
                    TempData["isloggedin"] = "true";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.LoginInfo = "Incorrect username or password";
                    TempData["isloggedin"] = "false";
                    return View();
                }
            }
            else
            {
                return View();
            }

        }
    }
}