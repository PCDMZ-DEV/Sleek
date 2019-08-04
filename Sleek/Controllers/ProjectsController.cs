#region "Imported Namespaces"

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sleek.Classes;
using Sleek.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace Sleek.Controllers {

    [Authorize]
    public class ProjectsController : Controller {

        #region "Variables and Constants"

        // Services
        private readonly MainContext Context;
        private readonly ILogger<ProjectsController> Logger;

        #endregion

        #region "Class Methods and Events"

        // Constructor
        public ProjectsController(MainContext context, ILogger<ProjectsController> logger) {
            Context = context;
            Logger = logger;
        }

        #endregion

        #region "Controller Methods and Events"

        // Index (Get)
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber) {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["ID"] = sortOrder == "ID" ? "ID_D" : "ID";
            ViewData["Date"] = sortOrder == "Date" ? "Date_D" : "Date";
            ViewData["Description"] = sortOrder == "Description" ? "Description_D" : "Description";

            if (searchString != null) {
                pageNumber = 1;
            } else {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var project = from p in Context.Project select p;
            if (!String.IsNullOrEmpty(searchString)) {
                project = project.Where(a => a.ProDescription.Contains(searchString));
            }
            switch (sortOrder) {
                case "Date":
                    project = project.OrderBy(a => a.ProDate);
                    break;
                case "Date_D":
                    project = project.OrderByDescending(a => a.ProDate);
                    break;
                case "Description":
                    project = project.OrderBy(a => a.ProDescription);
                    break;
                case "Description_D":
                    project = project.OrderByDescending(a => a.ProDescription);
                    break;
                case "ID":
                    project = project.OrderBy(a => a.ProId);
                    break;
                default:
                    project = project.OrderByDescending(a => a.ProId);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Project>.CreateAsync(project.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // Close
        public IActionResult Close() {
            return RedirectToAction("Index");
        }

        #endregion

    }

}
