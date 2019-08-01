#region "Imported Namespaces"

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sleek.Models;

#endregion

namespace Sleek.Controllers {

    [Authorize]
    public class HomeController : Controller {

        #region "Controller Actions"

        public IActionResult Index() {
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        #endregion

    }

}
