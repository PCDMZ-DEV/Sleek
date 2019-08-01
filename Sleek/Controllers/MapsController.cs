#region "Imported Namespaces"

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

#endregion

namespace Sleek.Controllers {

    public class MapsController : Controller {

        #region "Class Methods and Events"

        // Services
        public IConfiguration Configuration;
        public ILogger<IconsController> Logger;

        // Constructor
        public MapsController(IConfiguration configuration, ILogger<IconsController> logger) {
            Configuration = configuration;
            Logger = logger;
        }

        #endregion

        #region "Controller Methods and Events"

        // Google
        public IActionResult Google() {
            return View();
        }

        // Vector
        public IActionResult Vector() {
            return View();
        }

        #endregion

    }

}
