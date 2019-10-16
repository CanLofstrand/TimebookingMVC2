using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DayPilot.Web.Mvc.Events.Calendar;
using TimebookingMVC2.Api;
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events;
using TimebookingMVC2.Api.Models;

namespace TimebookingMVC2.Controllers
{
    public class HomeController : Controller
    {
        public static bool IsLoggedIn { get; set; } = false;
        public static string Token { get; set; }
        public static string Username { get; set; }
        public ActionResult Index()
        {
            var isLoggedIn = TempData["isloggedin"] as string;
            if (isLoggedIn == "true")
            {
                IsLoggedIn = true;
            }
            else
                IsLoggedIn = false;
            Token = TempData["token"] as string;


            ViewBag.isloggedin = IsLoggedIn;

            return View();
        }

        public ActionResult Backend()
        {
            return new Calendar().CallBack(this);
        }

        class Calendar : DayPilotCalendar
        {
            protected override void OnInit(InitArgs e)
            {
                Update(CallBackUpdateType.Full);
            }

            protected override void OnTimeRangeSelected(TimeRangeSelectedArgs e)
            {
                var toBeCreated = new Booking
                {
                    Date = e.Start,
                    UserName = Username
                };

                var api = new ApiCommunication();

                if (e.Start.Hour >= 8 && e.End.Hour <= 17)
                {
                    var response = api.PostBooking(toBeCreated, Token);
                }

                Update();
            }

            protected override void OnEventClick(EventClickArgs e)
            {
                var toBeDeleted = Convert.ToInt32(e.Id);

                var api = new ApiCommunication();

                var response = api.DeleteBooking(toBeDeleted, Token);

                Update();
            }

            protected override void OnFinish()
            {
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                var api = new ApiCommunication();

                var response = api.GetBookingsAsync(Token);

                if (HomeController.IsLoggedIn)
                {
                    Events = response.Result;
                }
                else
                    Events = null;

                DataIdField = "Id";
                DataTextField = "UserName";
                DataStartField = "Date";
                DataEndField = "EndDate";
            }

        }
    }
}