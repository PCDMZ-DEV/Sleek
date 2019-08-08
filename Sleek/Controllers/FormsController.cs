#region "Usings"

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

#endregion

namespace Sleek.Controllers {

    public class FormsController : Controller {

        #region "Class Methods and Events"

        // Services
        public IConfiguration Configuration;
        public ILogger<IconsController> Logger;

        // Constructor
        public FormsController(IConfiguration configuration, ILogger<IconsController> logger) {
            Configuration = configuration;
            Logger = logger;
        }

        #endregion

        #region "Controller Methods and Events"

        // Basic
        public IActionResult Basic() {
            return View();
        }

        // Input
        public IActionResult Input() {
            return View();
        }

        // Checkbox
        public IActionResult Checkbox() {
            return View();
        }

        // Validation
        public IActionResult Validation() {
            return View();
        }

        // Advanced
        public IActionResult Advanced() {
            return View();
        }

        #endregion

    }

}
