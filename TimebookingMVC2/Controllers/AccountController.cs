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

        public async Task<ActionResult> Register(RegisterModel model) 
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
                    UserPassword = model.Password
                };

                var response = api.PostRegister(user);
                
                if (response == true)
                {
                    ViewBag.registered = "Account registered, you can now log in.";
                    return View();
                }
                else
                    ViewBag.registered = "An error occured during registration.";
                    return View();
            }
            else
            {
                return View();
            }
        }

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
                    TempData["token"] = token;
                    TempData["username"] = User.Identity.Name;
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