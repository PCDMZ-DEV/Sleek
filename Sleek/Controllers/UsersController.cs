﻿#region "Usings"

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sleek.Classes;
using Sleek.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace Sleek.Controllers {

    [Authorize]
    public class UsersController : Controller {

        #region "Variables and Constants"

        // Services
        private readonly MainContext Context;
        private readonly ILogger<UsersController> Logger;
        private IActivityLog ActivityLog;
        private ISite Site;

        #endregion

        #region "Class Methods and Events"

        // Constructor
        public UsersController(MainContext context, ILogger<UsersController> logger, IActivityLog activitylog, ISite site) {
            Context = context;
            Logger = logger;
            ActivityLog = activitylog;
            Site = site;
        }

        #endregion

        #region "Controller Actions"

        // Index (Get)
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber) {

            IQueryable<User> users = Enumerable.Empty<User>().AsQueryable();
            PaginatedList<User> result = null;

            try {

                // Clear Calling Action
                Site.Controller = null;
                Site.Action = null;

                ViewData["CurrentSort"] = sortOrder;
                ViewData["ID"] = sortOrder == "ID" ? "ID_D" : "ID";
                ViewData["First"] = sortOrder == "First" ? "First_D" : "First";
                ViewData["Last"] = sortOrder == "Last" ? "Last_D" : "Last";

                if (searchString != null) {
                    pageNumber = 1;
                } else {
                    searchString = currentFilter;
                }

                ViewData["CurrentFilter"] = searchString;

                users = from u in Context.User select u;
                if (!String.IsNullOrEmpty(searchString)) {
                    users = users.Where(a => a.UsrFirst.Contains(searchString) || a.UsrLast.Contains(searchString) || a.UsrEmail.Contains(searchString));
                }
                switch (sortOrder) {
                    case "First":
                        users = users.OrderBy(a => a.UsrFirst);
                        break;
                    case "First_D":
                        users = users.OrderByDescending(a => a.UsrFirst);
                        break;
                    case "Last":
                        users = users.OrderBy(a => a.UsrLast);
                        break;
                    case "Last_D":
                        users = users.OrderByDescending(a => a.UsrLast);
                        break;
                    case "ID_D":
                        users = users.OrderByDescending(a => a.UsrId);
                        break;
                    default:
                        users = users.OrderBy(a => a.UsrId);
                        break;
                }

                int pageSize = 10;
                result = await PaginatedList<User>.CreateAsync(users.AsNoTracking(), pageNumber ?? 1, pageSize);

            } catch (Exception ex) {
                Site.Messages.Enqueue(ex.Message);
                Logger.LogError(ex, ex.Message);
            }

            return View(result);

        }

        // New (Get)
        public IActionResult New() {
            User user = new User();
            try {
                user.UsrCusid = Convert.ToInt32(User.FindFirst("Cusid"));
                user.UsrRole = "User";
                user.UsrStaid = 10000;
            } catch (Exception ex) {
                Site.Messages.Enqueue(ex.Message);
                Logger.LogError(ex, ex.Message);
            }
            return View("Detail", user);
        }

        // Edit (Get)
        public async Task<IActionResult> Edit(int? id) {
            var result = new User();
            try {
                if (id == null) {
                    throw new Exception("Record ID missing. Manual navigation prohibited.");
                }
                result = await Context.User.FindAsync(id);
                if (result == null) {
                    throw new Exception("Record not found. It may have been deleted by another user.");
                }
                ViewBag.Status = Context.Status.ToList().OrderBy(u => u.StaDescription);
            } catch (Exception ex) {
                Site.Messages.Enqueue(ex.Message);
                Logger.LogError(ex, ex.Message);
            }
            return View("Detail", result);
        }

        // Save (Post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(int id, User user) {
            string Activity = "";
            try {
                if (ModelState.IsValid) {
                    if (id == 0) {
                        Context.Add(user);
                        Activity = "Added User";
                    } else {
                        if (id != user.UsrId) {
                            throw new Exception("Record ID exception. Manual navigation prohibited.");
                        }
                        Activity = "Updated User";
                        Context.Update(user);
                    }
                    await Context.SaveChangesAsync();
                    ActivityLog.Warning(String.Format("{0} ({1})", Activity, user.UsrId));
                    return RedirectToAction("Profile");
                }
            } catch (Exception ex) {
                Site.Messages.Enqueue(ex.Message);
                Logger.LogError(ex, ex.Message);
            }
            return View("Detail", user);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) {
            try {
                var user = await Context.User.FindAsync(id);
                if (user == null) {
                    throw new Exception("Record not found. It may have been deleted by another Administrator.");
                }
                Context.User.Remove(user);
                await Context.SaveChangesAsync();
            } catch (Exception ex) {
                Site.Messages.Enqueue(ex.Message);
                ActivityLog.Warning(String.Format("Deleted User ({0})", id));
            }
            return RedirectToAction("Index");
        }

        // Profile (Get)
        public async Task<IActionResult> Profile() {
            var result = new User();
            try {
                result = await Context.User.FindAsync(Site.User);
                if (result == null) {
                    throw new Exception("Record not found. It may have been deleted by another user.");
                }
                ViewBag.Status = Context.Status.ToList().OrderBy(u => u.StaDescription);
            } catch (Exception ex) {
                Site.Messages.Enqueue(ex.Message);
                Logger.LogError(ex, ex.Message);
            }
            return View("Profile", result);
        }

        // Close
        public IActionResult Close() {
            return RedirectToAction("Index");
        }

        #endregion

    }

}
