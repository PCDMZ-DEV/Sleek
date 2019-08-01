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
            ViewBag.Message = "Success";
            return View("Detail", new User());
        }

        // Edit (Get)
        public async Task<IActionResult> Edit(int? id) {
            var result = new User();
            String message = "Success";
            try {
                if (id == null) {
                    return NotFound();
                }
                result = await _context.User.FindAsync(id);
                if (result == null) {
                    throw new Exception("Record not found. It may have been deleted by another Administrator.");
                }
            } catch (Exception ex) {
                message = ex.Message;
                _logger.LogError(ex, message);
            }
            return View("Detail", result);
        }

        // Save (Post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(int id, User user) {
            Site.Message = "";
            try {
                if (ModelState.IsValid) {
                    if (id == 0) {
                        _context.Add(user);
                    } else {
                        if (id != user.UsrId) {
                            throw new Exception("Record ID exception. Manual navigation prohibited.");
                        }
                        _context.Update(user);
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Profile));
                }
            } catch (Exception ex) {
                Site.Message = ex.Message;
                _logger.LogError(ex, Site.Message);
            }
            return View("Detail", user);
        }

        // Delete (If needed use an Ajax POST and pass the Anti-Forgery Token in the BeforeSend event.)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) {
            String message = "Success";
            try {
                var user = await _context.User.FindAsync(id);
                if (user == null) {
                    throw new Exception("Record not found. It may have been deleted by another Administrator.");
                }
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            } catch (Exception ex) {
                message = ex.Message;
                _logger.LogError(ex, message);
            } finally {
                ViewBag.Message = message;
            }
            return RedirectToAction(nameof(Profile));
        }

        // Close
        public IActionResult Close() {
            return RedirectToAction("Profile");
        }

        #endregion

    }

}
