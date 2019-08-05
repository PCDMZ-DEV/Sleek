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
    public class ProjectsController : Controller {

        #region "Variables and Constants"

        // Services
        private readonly MainContext Context;
        private readonly ILogger<ProjectsController> Logger;

        #endregion

        #region "Class Methods and Events"

        // Constructor
        public ProjectsController(MainContext context, ILogger<ProjectsController> logger) {
            Context = context;
            Logger = logger;
        }

        #endregion

        #region "Controller Actions""

        // Index (Get)
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber) {

            ViewData["CurrentSort"] = sortOrder;
            ViewData["ID"] = sortOrder == "ID" ? "ID_D" : "ID";
            ViewData["Date"] = sortOrder == "Date" ? "Date_D" : "Date";
            ViewData["Description"] = sortOrder == "Description" ? "Description_D" : "Description";

            if (searchString != null) {
                pageNumber = 1;
            } else {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var projects = from p in Context.Project select p;
            if (!String.IsNullOrEmpty(searchString)) {
                projects = projects.Where(a => a.ProDescription.Contains(searchString));
            }
            switch (sortOrder) {
                case "Date":
                    projects = projects.OrderBy(a => a.ProDate);
                    break;
                case "Date_D":
                    projects = projects.OrderByDescending(a => a.ProDate);
                    break;
                case "Description":
                    projects = projects.OrderBy(a => a.ProDescription);
                    break;
                case "Description_D":
                    projects = projects.OrderByDescending(a => a.ProDescription);
                    break;
                case "ID_D":
                    projects = projects.OrderByDescending(a => a.ProId);
                    break;
                default:
                    projects = projects.OrderBy(a => a.ProId);
                    break;
            }

            int pageSize = 10;
            return View(await PaginatedList<Project>.CreateAsync(projects.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // New (Get)
        public IActionResult New() {
            var project = new Project {
                ProDate = DateTime.Now,
                ProStaid = 10000
            };

            ViewBag.ProStatus = ((from Status in Context.Status select Status).ToList()).OrderBy(s => s.StaDescription);

            return View("Edit", project);
        }

        // Edit (Get)
        public async Task<IActionResult> Edit(int? id) {
            Site.Message = "";
            var result = new Project();
            try {
                if (id == null) {
                    return NotFound();
                }
                result = await Context.Project.FindAsync(id);
                if (result == null) {
                    throw new Exception("Record not found. It may have been deleted by another user.");
                }

                ViewBag.ProStatus = ((from Status in Context.Status select Status).ToList()).OrderBy(s => s.StaDescription);

            } catch (Exception ex) {
                Site.Message = ex.Message;
                Logger.LogError(ex, Site.Message);
            }
            return View("Edit", result);
        }

        // Save (Post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(int id, Project project) {
            Site.Message = "";
            string Activity = "";
            try {
                if (ModelState.IsValid) {
                    if (id == 0) {
                        Context.Add(project);
                        Activity = "Added Project";
                    } else {
                        if (id != project.ProId) {
                            throw new Exception("Record ID exception. Manual navigation prohibited.");
                        }
                        Activity = "Updated Project";
                        Context.Update(project);
                    }
                    await Context.SaveChangesAsync();
                    Site.Log(Context, project.ProCusid, project.ProUsrid, String.Format("{0}: {1}", Activity, project.ProId), "Warn");
                    return RedirectToAction("Index");
                }
            } catch (Exception ex) {
                Site.Message = ex.Message;
                Logger.LogError(ex, Site.Message);
            }
            return View("Edit", project);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id) {
            Site.Message = "";
            try {
                var project = await Context.Project.FindAsync(id);
                if (project == null) {
                    throw new Exception("Record not found. It may have been deleted by another Administrator.");
                }
                Context.Project.Remove(project);
                await Context.SaveChangesAsync();
            } catch (Exception ex) {
                Site.Message = ex.Message;
                Logger.LogError(ex, Site.Message);
            }
            return RedirectToAction("Index");
        }

        // Detail
        public async Task<IActionResult> Detail(int id, string sortOrder, string currentFilter, string searchString, int? pageNumber) {
            Site.Message = "";
            var orders = from o in Context.Order select o;
            try {

                var project = await Context.Project.FindAsync(id);
                var status = await Context.Status.FindAsync(project.ProStaid);

                ViewData["Description"] = project.ProDescription;
                ViewData["Local"] = Site.Nz(project.ProLocalpath, "Not Specified");
                ViewData["Remote"] = Site.Nz(project.ProRemotepath, "Not Specified");
                ViewData["Source"] = Site.Nz(project.ProSourcepath, "Not Specified");
                ViewData["Status"] = status.StaDescription;

                ViewData["CurrentSort"] = sortOrder;
                ViewData["ID"] = sortOrder == "ID" ? "ID_D" : "ID";
                ViewData["Date"] = sortOrder == "Date" ? "Date_D" : "Date";
                ViewData["Subject"] = sortOrder == "Subject" ? "Subject_D" : "Subject";

                if (searchString != null) {
                    pageNumber = 1;
                } else {
                    searchString = currentFilter;
                }

                ViewData["CurrentFilter"] = searchString;

                if (!String.IsNullOrEmpty(searchString)) {
                    orders = orders.Where(o => o.OrdSubject.Contains(searchString) || o.OrdComments.Contains(searchString));
                }

                switch (sortOrder) {
                    case "Date":
                        orders = orders.OrderBy(o => o.OrdDate);
                        break;
                    case "Date_D":
                        orders = orders.OrderByDescending(o => o.OrdDate);
                        break;
                    case "Description":
                        orders = orders.OrderBy(o => o.OrdSubject);
                        break;
                    case "Description_D":
                        orders = orders.OrderByDescending(o => o.OrdSubject);
                        break;
                    case "ID":
                        orders = orders.OrderBy(o => o.OrdId);
                        break;
                    default:
                        orders = orders.OrderByDescending(o => o.OrdId);
                        break;
                }


            } catch (Exception ex) {
                Site.Message = ex.Message;
                Logger.LogError(ex, Site.Message);
            }

            int pageSize = 10;
            return View(await PaginatedList<Order>.CreateAsync(orders.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        // Close
        public IActionResult Close() {
            return RedirectToAction("Index");
        }

        #endregion

    }

}
