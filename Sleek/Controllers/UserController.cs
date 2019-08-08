#region "Usings"

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
    public class UserController : Controller {

        #region "Variables and Constants"

        // Services
        private readonly MainContext Context;
        private readonly ILogger<UserController> Logger;

        #endregion

        #region "Class Methods and Events"

        // Constructor
        public UserController(MainContext context, ILogger<UserController> logger) {
            Context = context;
            Logger = logger;
        }

        #endregion

        #region "Controller Actions"

        // Profile (Get)
        public async Task<IActionResult> Profile(string sortOrder, string currentFilter, string searchString, int? pageNumber) {

            var users = from u in Context.User select u;
            PaginatedList<User> result = null;

            try {

                ViewData["CurrentSort"] = sortOrder;
                ViewData["ID"] = sortOrder == "ID" ? "ID_D" : "ID";
                ViewData["First"] = sortOrder == "First" ? "First_D" : "First";
                ViewData["Last"] = sortOrder == "Last" ? "Last_D" : "Last";
                ViewData["Email"] = sortOrder == "Email" ? "Email_D" : "Email";

                if (searchString != null) {
                    pageNumber = 1;
                } else {
                    searchString = currentFilter;
                }

                ViewData["CurrentFilter"] = searchString;

                if (!String.IsNullOrEmpty(searchString)) {
                    users = users.Where(u => u.UsrFirst.Contains(searchString) || u.UsrLast.Contains(searchString));
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
                    case "Email":
                        users = users.OrderBy(a => a.UsrEmail);
                        break;
                    case "Email_D":
                        users = users.OrderByDescending(a => a.UsrEmail);
                        break;
                    case "ID_D":
                        users = users.OrderByDescending(a => a.UsrId);
                        break;
                    default:
                        users = users.OrderBy(a => a.UsrId);
                        break;
                }

                int pageSize = 12;
                result = await PaginatedList<User>.CreateAsync(users.AsNoTracking(), pageNumber ?? 1, pageSize);

            } catch (Exception ex) {
                Site.Message = ex.Message;
                Logger.LogError(ex, Site.Message);
            }

            return View(result);
        }

        // New (Get)
        public IActionResult New() {
            Site.Message = "";
            User user = new User();
            try {
                user.UsrRole = "User";
                user.UsrStaid = 10000;
            } catch (Exception ex) {
                Site.Message = ex.Message;
                Logger.LogError(ex, Site.Message);
            }
            return View("Detail", user);
        }

        // Edit (Get)
        public async Task<IActionResult> Edit(int? id) {
            Site.Message = "";
            var result = new User();
            var status = Enumerable.Empty<Status>();
            try {
                if (id == null) {
                    throw new Exception("Record ID missing. Manual navigation prohibited.");
                }
                result = await Context.User.FindAsync(id);
                if (result == null) {
                    throw new Exception("Record not found. It may have been deleted by another user.");
                }
                status = Context.Status.ToList().OrderBy(u => u.StaDescription);
            } catch (Exception ex) {
                Site.Message = ex.Message;
                Logger.LogError(ex, Site.Message);
            }
            ViewBag.Status = status;
            return View("Detail", result);
        }

        // Save (Post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(int id, User user) {
            Site.Message = "";
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
                    Site.Log(Context, user.UsrCusid, user.UsrId, String.Format("{0}: {1}", Activity, user.UsrId), "Warn");
                    return RedirectToAction("Profile");
                }
            } catch (Exception ex) {
                Site.Message = ex.Message;
                Logger.LogError(ex, Site.Message);
            }
            return View("Detail", user);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) {
            Site.Message = "";
            try {
                var user = await Context.User.FindAsync(id);
                if (user == null) {
                    throw new Exception("Record not found. It may have been deleted by another Administrator.");
                }
                Context.User.Remove(user);
                await Context.SaveChangesAsync();
            } catch (Exception ex) {
                Site.Message = ex.Message;
                Logger.LogError(ex, Site.Message);
            }
            return RedirectToAction("Profile");
        }

        // Close
        public IActionResult Close() {
            return RedirectToAction("Profile");
        }

        #endregion

    }

}
