#region "Usings"

using Microsoft.AspNetCore.Http;
using Sleek.Models;
using System;

#endregion

namespace Sleek.Classes {

    #region "Interface"

    public interface IActivityLog {
        void Debug(string Content);
        void Information(string Content);
        void Warning(string Content);
        void Error(string Content);
        void Fatal(string Content);
        void Write(string Content, String Type);
    }

    #endregion

    public class ActivityLog : IActivityLog {

        #region "Variables"

        private MainContext Context = null;
        private IHttpContextAccessor HttpContext = null;
        private Object Customer = null;
        private Object User = null;

        #endregion

        #region "Class Methods"

        public ActivityLog(MainContext context, IHttpContextAccessor httpcontext) {
            Context = context;
            HttpContext = httpcontext;

            HttpContext c = HttpContext.HttpContext;
            if (c.User.Identity.IsAuthenticated) {
                Customer = Convert.ToInt32(c.User.FindFirst("Cusid").Value);
                User = Convert.ToInt32(c.User.FindFirst("Usrid").Value);
            }

        }

        #endregion

        #region "Overloads"

        public void Debug(string Content) {
            Write(Content, "Debug");
        }

        public void Information(string Content) {
            Write(Content, "Information");
        }

        public void Warning(string Content) {
            Write(Content, "Warning");
        }

        public void Error(string Content) {
            Write(Content, "Error");
        }

        public void Fatal(string Content) {
            Write(Content, "Fatal");
        }

        public void Write(string Description, string Type = "Warning") {
            try {
                Activity activity = new Activity {
                    ActDate = DateTime.Now,
                    ActCusid = (int)Customer,
                    ActUsrid = (int)User,
                    ActDescription = Description,
                    ActType = Type
                };
                Context.Update(activity);
                Context.SaveChanges();
            } catch (Exception ex) {
                // Log Error
            }
        }

        #endregion

    }

}
