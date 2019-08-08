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
    public class OrdersController : Controller {

        #region "Variables and Constants"

        // Services
        private readonly MainContext Context;
        private readonly ILogger<OrdersController> Logger;

        #endregion

        #region "Class Methods and Events"

        // Constructor
        public OrdersController(MainContext context, ILogger<OrdersController> logger) {
            Context = context;
            Logger = logger;
        }

        #endregion

        #region "Controller Actions""

        // New (Get)
        public IActionResult New() {
            var order = new Order {
                OrdDate = DateTime.Now,
                OrdStaid = 10000
            };

            ViewBag.OrdStatus = ((from Status in Context.Status select Status).ToList()).OrderBy(s => s.StaDescription);

            return View("Edit", order);
        }

        // Edit (Get)
        public async Task<IActionResult> Edit(int? id) {
            Site.Message = "";
            var result = new Order();
            try {

                result = await Context.Order.FindAsync(id);

                ViewBag.OrdStatus = ((from Status in Context.Status select Status).ToList()).OrderBy(s => s.StaDescription);

                ViewBag.Tools = new String[] { "Bold", "Italic", "Underline", "StrikeThrough",
                "FontName", "FontSize", "FontColor", "BackgroundColor",
                "LowerCase", "UpperCase", "|",
                "Formats", "Alignments", "OrderedList", "UnorderedList",
                "Outdent", "Indent", "|",
                "CreateLink", "Image", "|", "ClearFormat", "Print",
                "SourceCode", "FullScreen", "|", "Undo", "Redo" };

            } catch (Exception ex) {
                Site.Message = ex.Message;
                Logger.LogError(ex, Site.Message);
            }
            return View("Edit", result);
        }

        // Save (Post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(int id, Order order) {
            Site.Message = "";
            string Activity = "";
            try {
                if (ModelState.IsValid) {
                    if (id == 0) {
                        Context.Add(order);
                        Activity = "Added Work Order";
                    } else {
                        if (id != order.OrdId) {
                            throw new Exception("Record ID exception. Manual navigation prohibited.");
                        }
                        Activity = "Updated Work Order";
                        Context.Update(order);
                    }
                    await Context.SaveChangesAsync();
                    Site.Log(Context, order.OrdCusid, order.OrdUsrid, String.Format("{0}: {1}", Activity, order.OrdId), "Warn");
                    return RedirectToAction("Detail", "Projects");
                }
            } catch (Exception ex) {
                Site.Message = ex.Message;
                Logger.LogError(ex, Site.Message);
            }
            return View("Edit", order);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) {
            Site.Message = "";
            try {
                var order = await Context.Order.FindAsync(id);
                if (order == null) {
                    throw new Exception("Record not found. It may have been deleted by another Administrator.");
                }
                Context.Order.Remove(order);
                await Context.SaveChangesAsync();
            } catch (Exception ex) {
                Site.Message = ex.Message;
                Logger.LogError(ex, Site.Message);
            }
            return RedirectToAction("Detail", "Projects");
        }

        // Close
        public IActionResult Close(int id) {
            return RedirectToAction("Detail", "Projects", new {id});
        }

        #endregion

    }

}
