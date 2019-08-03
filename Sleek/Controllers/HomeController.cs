#region "Imported Namespaces"

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sleek.Models;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Sleek.Controllers {

    [Authorize]
    public class HomeController : Controller {

        #region "Variables and Constants"

        private MainContext Context;

        #endregion

        #region "Class Methods and Events"

        public HomeController(MainContext context) {
            Context = context;
        }

        #endregion

        #region "Controller Actions"

        public IActionResult Index() {
            return View();
        }

        public IActionResult Analytics() {
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        // Notifications
        public IActionResult Notifications() {
            var Notifications = Context.Activity.Where(a => a.ActCusid == Convert.ToInt32(User.FindFirst("cusid").Value)).OrderBy(a => a.ActDate);
            ViewBag.NotificationCount = Notifications.Count();
            ViewBag.Notifications = Notifications;
            return PartialView("_Notifications");
        }

        #endregion

    }

}
