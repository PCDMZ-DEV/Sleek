using Sleek.Models;
using System;

namespace Sleek.Classes {

    public class Site {

        #region "Website Settings"

        public static string Title = "Sleek (MVC)";
        public static string Description = "A fully functional implementation of the open-source Sleek Dashboard project developed with ASP.NET Core MVC";
        public static string Keywords = "";
        public static string Company = "Company Name";
        public static string Address = "123 Main Street";
        public static string CityStateZip = "Anytown, IN 12345";
        public static string Phone = "(317) 555-1212";
        public static string URL = "https://domain.com";
        public static string SalesAddress = "info@domain.com";
        public static string SalesName = "Company Name";
        public static string SupportAddress = "support@domain.com";
        public static string SupportName = "Company Name";
        public static string Copyright = string.Format("Copyright 2017 {0}, (Distributed under the MIT License)", Company);
        public static string Facebook = "";
        public static string Linkedin = "";
        public static string Twitter = "";
        public static string GitHub = "";
        public static string Xing = "";
        public static string Layout = "_Layout";
        public static string Mode = "Login";
        public static string Message = "";
        public static string ConnectionString = "Server=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Sleek.mdf;Database=Sleek;";

        #endregion

        #region "Methods and Events"

        public static bool Log(MainContext Context, int CustomerID, int UserID, string Description, string Type) {
            bool ReturnValue = true;
            try {
                Activity activity = new Activity();
                activity.ActDate = DateTime.Now;
                activity.ActCusid = CustomerID;
                activity.ActUsrid = UserID;
                activity.ActDescription = Description;
                activity.ActType = Type;
                Context.Update(activity);
                Context.SaveChanges();
            } catch (Exception ex) {
                Site.Message = ex.Message;
                ReturnValue = false;
            }
            return ReturnValue;
        }

        #endregion

    } // Class

} // Namespace
