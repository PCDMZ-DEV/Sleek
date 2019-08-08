#region "Usings"

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

#endregion

namespace Sleek.Controllers {

    public class IconsController : Controller {

        #region "Class Methods and Events"

        // Services
        public IConfiguration Configuration;
        public ILogger<IconsController> Logger;

        // Constructor
        public IconsController(IConfiguration configuration, ILogger<IconsController> logger) {
            Configuration = configuration;
            Logger = logger;
        }

        #endregion

        #region "Controller Methods and Events"

        // Material
        public IActionResult Material() {
            return View();
        }

        // Flag
        public IActionResult Flag() {
            return View();
        }

        #endregion

    }

}
