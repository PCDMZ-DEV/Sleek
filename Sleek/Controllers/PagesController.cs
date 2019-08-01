#region "Imported Namespaces"

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

#endregion

namespace Sleek.Controllers {

    public class PagesController : Controller {

        #region "Class Methods and Events"

        // Services
        public IConfiguration Configuration;
        public ILogger<IconsController> Logger;

        // Constructor
        public PagesController(IConfiguration configuration, ILogger<IconsController> logger) {
            Configuration = configuration;
            Logger = logger;
        }

        #endregion

        #region "Controller Methods and Events"

        // Profile
        public IActionResult Profile() {
            return View();
        }

        // Login
        public IActionResult Login() {
            return View();
        }

        // Registration
        public IActionResult Registration() {
            return View();
        }

        // Invoice
        public IActionResult Invoice() {
            return View();
        }

        // Error
        public IActionResult Error() {
            return View();
        }

        #endregion

    }

}
