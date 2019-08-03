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
    public class UserController : Controller {

        #region "Variables and Constants"

        // Services
        private readonly MainContext _context;
        private readonly ILogger<UserController> _logger;

        #endregion

        #region "Class Methods and Events"

        // Constructor
        public UserController(MainContext context, ILogger<UserController> logger) {
            _context = context;
            _logger = logger;
        }

        #endregion

        #region "Controller Methods and Events"

        // Profile (Get)
        public async Task<IActionResult> Profile(string sortOrder, string currentFilter, string searchString, int? pageNumber) {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["EmailSortParm"] = String.IsNullOrEmpty(sortOrder) ? "email_desc" : "";

            if (searchString != null) {
                pageNumber = 1;
            } else {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var users = from u in _context.User select u;
            if (!String.IsNullOrEmpty(searchString)) {
                users = users.Where(u => u.UsrFirst.Contains(searchString) || u.UsrLast.Contains(searchString));
            }
            switch (sortOrder) {
                case "email_desc":
                    users = users.OrderByDescending(u => u.UsrEmail);
                    break;
                default:
                    users = users.OrderBy(u => u.UsrEmail);
                    break;
            }

            int pageSize = 12;
            return View(await PaginatedList<User>.CreateAsync(users.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // New (Get)
        public IActionResult New() {
            return View("Detail", new User());
        }

        // Edit (Get)
        public async Task<IActionResult> Edit(int? id) {
            Site.Message = "";
            var result = new User();
            try {
                if (id == null) {
                    return NotFound();
                }
                result = await _context.User.FindAsync(id);
                if (result == null) {
                    throw new Exception("Record not found. It may have been deleted by another user.");
                }
            } catch (Exception ex) {
                Site.Message = ex.Message;
                _logger.LogError(ex, Site.Message);
            }
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
                        _context.Add(user);
                        Activity = "Added User";
                    } else {
                        if (id != user.UsrId) {
                            throw new Exception("Record ID exception. Manual navigation prohibited.");
                        }
                        Activity = "Updated User";
                        _context.Update(user);
                    }
                    await _context.SaveChangesAsync();
                    Site.Log(_context, user.UsrCusid, user.UsrId, String.Format("{0}: {1}", Activity, user.UsrId), "Warn");
                    return RedirectToAction("Profile");
                }
            } catch (Exception ex) {
                Site.Message = ex.Message;
                _logger.LogError(ex, Site.Message);
            }
            return View("Detail", user);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) {
            Site.Message = "";
            try {
                var user = await _context.User.FindAsync(id);
                if (user == null) {
                    throw new Exception("Record not found. It may have been deleted by another Administrator.");
                }
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            } catch (Exception ex) {
                Site.Message = ex.Message;
                _logger.LogError(ex, Site.Message);
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
