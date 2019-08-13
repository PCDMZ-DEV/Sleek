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

            IQueryable<Project> projects = Enumerable.Empty<Project>().AsQueryable();
            PaginatedList<Project> result = null;

            try {

                // Clear Calling Action
                Site.Controller = null;
                Site.Action = null;

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

                projects = from p in Context.Project select p;
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
                result = await PaginatedList<Project>.CreateAsync(projects.AsNoTracking(), pageNumber ?? 1, pageSize);

            } catch (Exception ex) {
                Site.Messages.Enqueue(ex.Message);
                Logger.LogError(ex, ex.Message);
            }

            return View(result);

        }

        // New (Get)
        public IActionResult New() {
            var project = new Project {
                ProDate = DateTime.Now,
                ProCusid = Convert.ToInt32(User.FindFirst("Cusid").Value),
                ProUsrid = Convert.ToInt32(User.FindFirst("Usrid").Value),
                ProStaid = 10000
            };

            ViewBag.ProStatus = ((from Status in Context.Status select Status).ToList()).OrderBy(s => s.StaDescription);

            return View("Edit", project);
        }

        // Edit (Get)
        public async Task<IActionResult> Edit(int? id) {
            var result = new Project();
            try {

                if (id == null) {
                    throw new ApplicationException("Record ID missing.");
                }

                result = await Context.Project.FindAsync(id);
                if (result == null) {
                    throw new ApplicationException("Record not found.");
                }

                ViewBag.ProStatus = ((from Status in Context.Status select Status).ToList()).OrderBy(s => s.StaDescription);

            } catch (Exception ex) {
                Site.Messages.Enqueue(ex.Message);
                Logger.LogError(ex, ex.Message);
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
                    Logger.LogWarning("User {user} created Project {project}", project.ProUsrid, project.ProId);
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
            IQueryable<Order> orders = Enumerable.Empty<Order>().AsQueryable();
            try {

                var project = await Context.Project.FindAsync(id);
                Context.Entry(project).Reference(c => c.Customer).Load();
                Context.Entry(project).Reference(u => u.User).Load();
                Context.Entry(project).Reference(s => s.Status).Load();

                ViewData["Project"] = project;

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

                orders = from o in Context.Order select o;
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
                    case "Subject":
                        orders = orders.OrderBy(o => o.OrdSubject);
                        break;
                    case "Subject_D":
                        orders = orders.OrderByDescending(o => o.OrdSubject);
                        break;
                    case "ID_D":
                        orders = orders.OrderByDescending(o => o.OrdId);
                        break;
                    default:
                        orders = orders.OrderBy(o => o.OrdId);
                        break;
                }


            } catch (Exception ex) {
                Site.Message = ex.Message;
                Logger.LogError(ex, Site.Message);
            }

            int pageSize = 10;
            return View(await PaginatedList<Order>.CreateAsync(orders.AsNoTracking(), pageNumber ?? 1, pageSize));

        }

        #endregion

    }

}
