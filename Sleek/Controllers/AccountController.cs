#region "Imported Namespaces"

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly SmtpClient SmtpClient;

        #endregion

        #region "Class Methods and Events"

        // Constructor
        public AccountController(MainContext context, IConfiguration configuration, SmtpClient smtpclient) {
            Context = context;
            Configuration = configuration;
            SmtpClient = smtpclient;
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
            Site.Mode = "Register";
            return View("Register");
        }

        // Register (Post)
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([FromForm]Register registration) {
            String message = "Registration Failed";
            Site.Mode = "Register";
            try {
                if (ModelState.IsValid) {
                    var user = Context.User.Where(u => u.UsrEmail == registration.RegEmail).FirstOrDefault();
                    if (user == null) {
                        user = new User {
                            UsrFirst = registration.RegFirst,
                            UsrLast = registration.RegLast,
                            UsrEmail = registration.RegEmail,
                            UsrPassword = registration.RegPassword,
                            UsrStaid = 10000,
                            UsrRole = "User",
                            UsrToken = "",
                            UsrTokendate = DateTime.Now
                        };
                        Context.Add(user);
                        Context.SaveChanges();
                        message = "Success, Registration successful. Please respond to the confirmation message we just sent you. Unpon confirmation you can sign in to your account.";
                        Site.Mode = "Login";
                    } else {
                        message = "Warning, We already have that E-Mail address on file. Please sign in to your account.";
                        Site.Mode = "Login";
                    }
                }
            } catch (Exception ex) {
                message = string.Format("{0}, {1}", "Error", ex.Message);
            }
            ViewBag.Message = message;
            return View("Register", registration);
        }

        // Recover Password (Get)
        [AllowAnonymous]
        public IActionResult Recover() {
            Site.Mode = "Recover";
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
                Site.Mode = "Login";
                return RedirectToAction("Login", "Home");
            }
            Site.Mode = "Recover";
            return View("Recover", model);
        }

        // Login (Get)
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null) {
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Account");
            }
            ViewData["ReturnUrl"] = returnUrl;
            Site.Mode = "Login";
            return View("Login");
        }

        // Login (Post)
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm]Login model) {
            Site.Message = "";
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
                                new Claim("facebook", "https://www.facebook.com/peerfoods"),
                                new Claim("linkedin", "https://www.linkedin.com/in/peerfoods"),
                                new Claim("twitter", "https://twitter.com/peerfoods?lang=en"),
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
                        Site.Log(Context, user.UsrCusid, user.UsrId, "Signed In", "Info");
                        return RedirectToAction("Index", "Home");

                    } else {
                        Site.Message = "Password does not match our records";
                    }
                } else {
                    Site.Message = "E-Mail Address not found.";
                }
            }
            return View("Login", model);
        }

        // Logout (Get)
        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var activity = new Activity();
            Site.Log(Context, Convert.ToInt32(User.FindFirst("cusid").Value), Convert.ToInt32(User.FindFirst("usrid").Value), "Signed Out", "Info");
            return RedirectToAction("Login", "Account");
        }

        #endregion

    }

}
