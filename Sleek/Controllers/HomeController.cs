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
            ViewData["ID"] = sortOrder == "ID" ? "ID_D" : "ID";
            ViewData["Date"] = sortOrder == "Date" ? "Date_D" : "Date";
            ViewData["Description"] = sortOrder == "Description" ? "Description_D" : "Description";
            ViewData["Type"] = sortOrder == "Type" ? "Type_D" : "Type";

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
                case "Type":
                    activity = activity.OrderBy(a => a.ActType);
                    break;
                case "Type_D":
                    activity = activity.OrderByDescending(a => a.ActType);
                    break;
                case "Date":
                    activity = activity.OrderBy(a => a.ActDate);
                    break;
                case "Date_D":
                    activity = activity.OrderByDescending(a => a.ActDate);
                    break;
                case "Description":
                    activity = activity.OrderBy(a => a.ActDescription);
                    break;
                case "Description_D":
                    activity = activity.OrderByDescending(a => a.ActDescription);
                    break;
                case "ID":
                    activity = activity.OrderBy(a => a.ActId);
                    break;
                default:
                    activity = activity.OrderByDescending(a => a.ActId);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Activity>.CreateAsync(activity.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        #endregion

    }

}
