#region "Usings"

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

#endregion

namespace Sleek.Controllers {

    public class WidgetsController : Controller {

        #region "Class Methods and Events"

        // Services
        public IConfiguration Configuration;
        public ILogger<IconsController> Logger;

        // Constructor
        public WidgetsController(IConfiguration configuration, ILogger<IconsController> logger) {
            Configuration = configuration;
            Logger = logger;
        }

        #endregion

        #region "Controller Methods and Events"

        // General
        public IActionResult General() {
            return View();
        }

        // Chart
        public IActionResult Chart() {
            return View();
        }

        #endregion

    }

}
