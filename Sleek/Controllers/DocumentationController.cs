#region "Usings"

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sleek.Classes;

#endregion

namespace Sleek.Controllers {

    public class DocumentationController : Controller {

        #region "Class Methods and Events"

        // Services
        public IConfiguration Configuration;
        public ILogger<DocumentationController> Logger;

        // Constructor
        public DocumentationController(IConfiguration configuration, ILogger<DocumentationController> logger) {
            Configuration = configuration;
            Logger = logger;
        }

        #endregion

        #region "Getting Started"

        // Introduction
        public IActionResult Introduction() {
            return View();
        }

        // Quick Start
        public IActionResult Quick_Start() {
            return View();
        }

        // Customization
        public IActionResult Customization() {
            return View();
        }

        // Model View Controller
        public IActionResult Model_View_Controller() {
            return View();
        }

        #endregion

        #region "Header Variations"

        public IActionResult Header_Fixed() {
            return View();
        }

        public IActionResult Header_Static() {
            return View();
        }

        public IActionResult Header_Light() {
            return View();
        }

        public IActionResult Header_Dark() {
            return View();
        }

        #endregion

        #region "Sidebar Variations"

        public IActionResult Sidebar_Fixed_Default() {
            return View();
        }

        public IActionResult Sidebar_Fixed_Minified() {
            return View();
        }

        public IActionResult Sidebar_Fixed_Offcanvas() {
            return View();
        }

        public IActionResult Sidebar_Static_Default() {
            return View();
        }


        public IActionResult Sidebar_Static_Minified() {
            return View();
        }

        public IActionResult Sidebar_Static_Offcanvas() {
            return View();
        }

        public IActionResult Sidebar_With_Footer() {
            return View();
        }

        public IActionResult Sidebar_Without_Footer() {
            return View();
        }

        public IActionResult Sidebar_Right() {
            return View();
        }

        #endregion

    }

}
