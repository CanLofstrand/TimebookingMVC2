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
        public bool IsLoggedIn { get; set; } = false;
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
            Username = TempData["username"] as string;

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
                var toBeCreated = new BookingWEndDate
                {
                    Date = e.Start,
                    EndDate = e.End,
                    UserName = Username
                };

                //post booking

                Update();
            }

            protected override void OnFinish()
            {
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                var api = new ApiCommunication();

                var response = api.GetBookingsAsync("NtT7_WAEH95Xmu9mz15ChFts36RWjCJtIyFOQb1-yXZIuVSi4IIrwQMjgomWbZPtBDnuJHT_g3hdvbw6Iwix2T4h02gYKo624Rp2Wcuj9jyCFBVNTdiPfeakMFnl7BZGfL4PpEvYTHknPdAQx2mG_T1brjmWdBGLFv-Ef1Fl_nVQ7nN2-1ZKQoCXVLO_zAk5fqSkV03-BPEf4SwlBahCW32Cq0euYO4i6c9WkIOFiJspuuKQpFAPiuBhp_POQ8nHSgqUNA0a251Jxjntkdf1L23-lvHLGKhiMshlFaKfj7m-DT22saNQTz0GDlL-76EdkME2fK-Q__zpmQvigV8byw");

                Events = response.Result;

                DataIdField = "Id";
                DataTextField = "UserName";
                DataStartField = "Date";
                DataEndField = "EndDate";
            }

        }
    }
}