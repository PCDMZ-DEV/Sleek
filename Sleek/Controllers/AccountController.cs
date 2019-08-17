#region "Usings"

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Sleek.Classes;
using Sleek.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

#endregion

namespace Sleek.Controllers {

    [Authorize]
    public class AccountController : Controller {

        #region "Variables and Constants"

        // Variables
        private readonly MainContext Context;
        private readonly IConfiguration Configuration;
        private SmtpClient SmtpClient;
        private ILogger<AccountController> Logger;
        private IActivityLog ActivityLog;
        private ISite Site;

        #endregion

        #region "Class Methods and Events"

        // Constructor
        public AccountController(
                MainContext context, 
                IConfiguration configuration, 
                SmtpClient smtpclient, 
                ILogger<AccountController> logger, 
                IActivityLog activitylog,
                ISite site) {
            Context = context;
            Configuration = configuration;
            SmtpClient = smtpclient;
            Logger = logger;
            ActivityLog = activitylog;
            Site = site;
        }

        #endregion

        #region "Controller Actions"

        // Index
        public IActionResult Index() {
            return View();
        }

        // Register (Get)
        [AllowAnonymous]
        public IActionResult Register() {
            return View("Register");
        }

        // Register (Post)
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([FromForm]Register registration) {
            try {
                if (ModelState.IsValid) {
                    var customer = Context.Customer.Where(u => u.CusEmail == registration.CusEmail).FirstOrDefault();
                    if (customer == null) {
                        customer = new Customer {
                            CusCompany = registration.CusCompany,
                            CusFirst = registration.CusFirst,
                            CusLast = registration.CusLast,
                            CusAddress1 = registration.CusAddress1,
                            CusAddress2 = registration.CusAddress2,
                            CusCity = registration.CusCity,
                            CusState = registration.CusState,
                            CusZip = registration.CusZip,
                            CusPhone = registration.CusPhone,
                            CusEmail = registration.CusEmail,
                            CusStaid = 10009 // Pending
                        };
                        Context.Add(customer);
                        Context.SaveChanges();
                        ActivityLog.Warning(String.Format("New Registration ({0})", customer.CusId));
                        throw new ApplicationException("Registration successful. Please respond to the confirmation message we just sent you. Unpon confirmation you can sign in to your account");
                    } else {
                        throw new ApplicationException("We already have that E-Mail address on file. Please sign in to your account");
                    }
                }
            } catch (Exception ex) {
                Site.Messages.Enqueue(ex.Message);
                Logger.LogError(ex, ex.Message);
            }
            return View("Register", registration);
        }

        // Recover Password (Get)
        [AllowAnonymous]
        public IActionResult Recover() {
            return PartialView("_Recover");
        }

        // Recover Password (Post)
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Recover(User model, string returnUrl = null) {
            if (ModelState.IsValid) {
                var message = new MailMessage(Configuration["Smtp:From"], model.UsrEmail, "Password Recovery", "This method is incomplete. Store a token and expiration date in the user record, validate and send, and then authenticate if the response is prior to expiration.");
                SmtpClient.Send(message);
                ActivityLog.Warning("Password Recovery");
                return RedirectToAction("Login", "Home");
            }
            return View("Recover", model);
        }

        // Login (Get)
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null) {
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Account");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View("Login");
        }

        // Login (Post)
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm]Login model) {
            try {
                if (ModelState.IsValid) {
                    User user = Context.User.SingleOrDefault(u => u.UsrEmail == model.Username);
                    if (user != null) {
                        if (user.UsrPassword == model.Password) {
                            var claims = new List<Claim> {
                                new Claim("cusid", user.UsrCusid.ToString()),
                                new Claim("usrid", user.UsrId.ToString()),
                                new Claim("full", string.Format("{0} {1}", user.UsrFirst, user.UsrLast)),
                                new Claim("first", user.UsrFirst),
                                new Claim("last", user.UsrLast),
                                new Claim("email", user.UsrEmail),
                                new Claim("facebook", "https://www.facebook.com/pcdmz"),
                                new Claim("linkedin", "https://www.linkedin.com/in/pcdmz"),
                                new Claim("twitter", "https://twitter.com/pcdmz?lang=en"),
                                new Claim("skype", ""),
                                new Claim("xing", ""),
                                new Claim("role", user.UsrRole),
                                new Claim(ClaimTypes.Email, model.Username)
                            };
                            var userIdentity = new ClaimsIdentity(claims, "login");
                            ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                            await HttpContext.SignInAsync(
                                CookieAuthenticationDefaults.AuthenticationScheme,
                                principal,
                                new AuthenticationProperties {
                                    IsPersistent = true
                                });
                            ActivityLog.Warning("Signed In");
                            return RedirectToAction("Index", "Home");

                        } else {
                            throw new ArgumentException("Password does not match our records");
                        }
                    } else {
                        throw new ArgumentException("E-Mail Address not found");
                    }
                }
            } catch (Exception ex) {
                Site.Messages.Enqueue(ex.Message);
                Logger.LogError(ex, ex.Message);
            }
            return View("Login", model);
        }

        // Logout (Get)
        public async Task<IActionResult> Logout() {
            try {
                var user = await Context.User.FindAsync(Convert.ToInt32(User.FindFirst("cusid").Value));
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                ActivityLog.Warning("Signed Out");
            } catch (Exception ex) {
                Site.Messages.Enqueue(ex.Message);
                Logger.LogError(ex, ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }

        #endregion

    }

}
