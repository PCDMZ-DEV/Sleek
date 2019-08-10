#region "Usings"

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
        public IActionResult New(int? id) {
            Order order = new Order();
            try {
                order.OrdDate = DateTime.Now;
                order.OrdProid = Convert.ToInt32(id);
                order.OrdStaid = 10000;
                ViewBag.OrdStatus = ((from Status in Context.Status select Status).ToList()).OrderBy(s => s.StaDescription);
            } catch (Exception ex) {
                Site.Messages.Enqueue(ex.Message);
                Logger.LogError(ex, ex.Message);
            }
            return View("Edit", order);
        }

        // Edit (Get)
        public async Task<IActionResult> Edit(int? id) {
            var order = new Order();
            try {
                order = await Context.Order.FindAsync(id);
                if (order == null) {
                    throw new ApplicationException("Work Order not found");
                }
                Context.Entry(order).Reference(u => u.User).Load();
                Context.Entry(order).Reference(p => p.Project).Load();
                Context.Entry(order).Reference(s => s.Status).Load();
                ViewBag.OrdStatus = ((from Status in Context.Status select Status).ToList()).OrderBy(s => s.StaDescription);
            } catch (Exception ex) {
                Site.Messages.Enqueue(ex.Message);
                Logger.LogError(ex, ex.Message);
            }
            return View("Edit", order);
        }

        // Save (Post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(int id, Order order) {
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
                    return RedirectToAction("Detail", "Projects", new { id = order.OrdProid });
                }
            } catch (Exception ex) {
                Site.Messages.Enqueue(ex.Message);
                Logger.LogError(ex, ex.Message);
            }
            return View("Edit", order);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) {
            try {
                var order = await Context.Order.FindAsync(id);
                if (order == null) {
                    throw new Exception("Work Order not found.");
                }
                Context.Order.Remove(order);
                await Context.SaveChangesAsync();
            } catch (Exception ex) {
                Site.Messages.Enqueue(ex.Message);
                Logger.LogError(ex, ex.Message);
            }
            return RedirectToAction("Detail", "Projects", new { id });
        }

        #endregion

    }

}
