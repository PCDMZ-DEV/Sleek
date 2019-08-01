#region "Imported Namespaces"

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sleek.Classes;

#endregion

namespace Sleek.Controllers {

    public class ChartsController : Controller {

        #region "Class Methods and Events"

        // Services
        public IConfiguration Configuration;
        public ILogger<ChartsController> Logger;

        // Constructor
        public ChartsController(IConfiguration configuration, ILogger<ChartsController> logger) {
            Configuration = configuration;
            Logger = logger;
        }

        #endregion

        #region "Controller Methods and Events"

        // Chartjs (Get)
        public IActionResult Chartjs() {
            return View();
        }

        #endregion

    }

}
