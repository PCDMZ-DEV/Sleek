#region "Usings"

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sleek.Classes;

#endregion

namespace Sleek.Controllers {

    public class ComponentsController : Controller {

        #region "Class Methods and Events"

        // Services
        public IConfiguration Configuration;
        public ILogger<ComponentsController> Logger;

        // Constructor
        public ComponentsController(IConfiguration configuration, ILogger<ComponentsController> logger) {
            Configuration = configuration;
            Logger = logger;
        }

        #endregion

        #region "Controller Methods and Events"

        // Index (Get)
        public IActionResult Index() {
            return View();
        }

        // Alerts
        public IActionResult Alerts() {
            return View();
        }

        // Badges
        public IActionResult Badges() {
            return View();
        }

        // Breadcrumbs
        public IActionResult Breadcrumbs() {
            return View();
        }

        // Buttons
        public IActionResult Buttons() {
            return View();
        }

        // Dropdowns
        public IActionResult Dropdowns() {
            return View();
        }

        // Groups
        public IActionResult Groups() {
            return View();
        }

        // Social
        public IActionResult Social() {
            return View();
        }

        // Loading
        public IActionResult Loading() {
            return View();
        }

        // Cards
        public IActionResult Cards() {
            return View();
        }

        // Carousels
        public IActionResult Carousels() {
            return View();
        }

        // Collapse
        public IActionResult Collapse() {
            return View();
        }

        // Lists
        public IActionResult Lists() {
            return View();
        }

        // Modals
        public IActionResult Modals() {
            return View();
        }

        // Pagination
        public IActionResult Pagination() {
            return View();
        }

        // Tooltips
        public IActionResult Tooltips() {
            return View();
        }

        // Progress
        public IActionResult Progress() {
            return View();
        }

        // Spinners
        public IActionResult Spinners() {
            return View();
        }

        // Switchers
        public IActionResult Switchers() {
            return View();
        }

        // Tables
        public IActionResult Tables() {
            return View();
        }

        // Tabs
        public IActionResult Tabs() {
            return View();
        }

        #endregion

    }

}
