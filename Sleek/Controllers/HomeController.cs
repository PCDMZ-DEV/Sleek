#region "Imported Namespaces"

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sleek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        // Activity (Get)
        public async Task<IActionResult> Activity(string sortOrder, string currentFilter, string searchString, int? pageNumber) {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["TypeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";

            if (searchString != null) {
                pageNumber = 1;
            } else {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var activity = from a in Context.Activity select a;
            if (!String.IsNullOrEmpty(searchString)) {
                activity = activity.Where(a => a.ActDescription.Contains(searchString) || a.ActType.Contains(searchString));
            }
            switch (sortOrder) {
                case "type_desc":
                    activity = activity.OrderByDescending(a => a.ActType);
                    break;
                default:
                    activity = activity.OrderBy(a => a.ActType);
                    break;
            }

            int pageSize = 12;
            return View(await PaginatedList<Activity>.CreateAsync(activity.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        #endregion

    }

}
