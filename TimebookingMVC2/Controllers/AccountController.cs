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
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!string.IsNullOrEmpty(model.UserName) && !string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Password))
            {
                // Send to api and await getting OK
                Api.Models.User user = new User()
                {
                    UserName = model.UserName,
                    UserEmail = model.Email,
                    UserPassword = model.Password
                };

                // TODO: Error message/something else if could not be created
                // Ok message if created (or just log in user?)
                // Possibly more results from apicomm for different errors
                if (_apiComm.Post(user))
                    ; // User created
                else
                    ; // Not created

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
                return View();
            }
            else
            {
                return View();
            }

        }
    }
}