#region "Imported Namespaces"

using Microsoft.AspNetCore.Http;
using Sleek.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#endregion

namespace Sleek.Classes {

    #region "Interface"

    public interface ISite {
        Object Customer { get; set; }
        Object User { get; set; }
        string Controller { get; set; }
        string Action { get; set; }
        Queue<string> Messages { get; set; }

        string Title { get; set; }
        string Description { get; set; }
        string Keywords { get; set; }
        string Company { get; set; }
        string Address { get; set; }
        string CityStateZip { get; set; }
        string Phone { get; set; }
        string URL { get; set; }
        string SalesAddress { get; set; }
        string SalesName { get; set; }
        string SupportAddress { get; set; }
        string SupportName { get; set; }
        string Copyright { get; set; }
        string Facebook { get; set; }
        string Linkedin { get; set; }
        string Twitter { get; set; }
        string GitHub { get; set; }
        string Xing { get; set; }
        string Layout { get; set; }
        string ConnectionString { get; set; }

    }

    #endregion

    public class Site : ISite {

        #region "Variables and Constants"

        private readonly MainContext Context = null;
        private IHttpContextAccessor HttpContext = null;

        #endregion

        #region "Properties"

        public Object Customer { get; set; }
        public Object User { get; set; }
        public Queue<string> Messages { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string CityStateZip { get; set; }
        public string Phone { get; set; }
        public string URL { get; set; }
        public string SalesAddress { get; set; }
        public string SalesName { get; set; }
        public string SupportAddress { get; set; }
        public string SupportName { get; set; }
        public string Copyright { get; set; }
        public string Facebook { get; set; }
        public string Linkedin { get; set; }
        public string Twitter { get; set; }
        public string GitHub { get; set; }
        public string Xing { get; set; }
        public string Layout { get; set; }
        public string ConnectionString { get; set; }

        #endregion

        #region "Class Methods"

        public Site(MainContext context, IHttpContextAccessor httpcontext) {
            Context = context;
            HttpContext = httpcontext;

            Title = "Sleek (MVC)";
            Description = "A fully functional implementation of the open-source Sleek Dashboard project developed with ASP.NET Core MVC";
            Keywords = "";
            Company = "Company Name";
            Address = "123 Main Street";
            CityStateZip = "Anytown, IN 12345";
            Phone = "(317) 555-1212";
            URL = "https://domain.com";
            SalesAddress = "info@domain.com";
            SalesName = "Company Name";
            SupportAddress = "support@domain.com";
            SupportName = "Company Name";
            Copyright = string.Format("Copyright 2017 {0}, (Distributed under the MIT License)", Company);
            Facebook = "";
            Linkedin = "";
            Twitter = "";
            GitHub = "";
            Xing = "";
            Layout = "_Layout";
            ConnectionString = "Server=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Sleek.mdf;Database=Sleek;";

            Messages = new Queue<string>();

        HttpContext c = HttpContext.HttpContext;
            if (c.User.Identity.IsAuthenticated) {
                Customer = Convert.ToInt32(c.User.FindFirst("Cusid").Value);
                User = Convert.ToInt32(c.User.FindFirst("Usrid").Value);
            }

}

#endregion

#region "Methods"

/// <summary>
/// BackupFileName
/// </summary>
/// <param name="FileName">String representing the file name to format</param>
/// <returns>Original File Name with Year, Month Date and Time prepended</returns>
public static string BackupFileName(object FileName) {
    string ReturnValue = null;
    DateTime CurrentTime = DateTime.Now;
    try {
        ReturnValue = CurrentTime.Year.ToString() + "." + CurrentTime.Month.ToString("D2") + "." + CurrentTime.Day.ToString("D2") + "-" + CurrentTime.Hour.ToString("D2") + CurrentTime.Minute.ToString("D2") + "-" + FileName;
    } catch {
        ReturnValue = null;
    }
    return ReturnValue;
}

/// <summary>
/// Nl2br
/// </summary>
/// <param name="text"></param>
/// <returns>String with all new line characters replaced with <br /></returns>
public static string Nl2br(string text) {
    return text.Replace("\r\n", "<br />").Replace("\n", "<br />");
}

/// <summary>
/// BytesToString
/// </summary>
/// <param name="byteCount">The number to format as a Long Integer value</param>
/// <returns>A human readable representation of the input value as a string</returns>
/// <remarks>From StackOverflow.com</remarks>
public static String BytesToString(long byteCount) {
    string[] suf = new[] { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
    if (byteCount == 0)
        return "0 " + suf[0];
    long bytes = Math.Abs(byteCount);
    int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
    double num = Math.Round(bytes / Math.Pow(1024, place), 1);
    return (Math.Sign(byteCount) * num).ToString() + " " + suf[place];
}

/// <summary>
/// Megabytes
/// </summary>
/// <param name="Kilobytes">A double depicting the number of Kilobytes to evaluate</param>
/// <returns>Double with input converted to Megabytes</returns>
public static double Megabytes(double Kilobytes) {
    return Kilobytes / (double)1048576;
}

/// <summary>
/// RightIndexOf - Retrieve the rightmost portion of a string beginning with the first or last occurance of the specified string index character
/// </summary>
/// <param name="SourceString">String containing the content to be evaluated</param>
/// <param name="StringIndex">String depicting the index value to be located</param>
/// <param name="LastOccurance">Boolean indicating if the first or last occurence of the index should be used for processing</param>
/// <returns>The right n characters of the source string starting at the first or last occurence of a string index</returns>
public static string RightIndexOf(string SourceString, string StringIndex, bool LastOccurance = true) {
    string ReturnValue = SourceString;
    try {
        int StartPosition;
        if (LastOccurance)
            StartPosition = SourceString.LastIndexOf(StringIndex) + 1;
        else
            StartPosition = SourceString.IndexOf(StringIndex) + 1;
        int EndPosition = SourceString.Length - StartPosition;
        ReturnValue = SourceString.Substring(StartPosition, EndPosition);
    } catch {
    }
    return ReturnValue;
}

/// <summary>
/// LeftIndexOf - Retrieve the leftmost portion of a string ending with the first occurence of the specified string index character
/// </summary>
/// <param name="SourceString">String containging the content to be evaluated</param>
/// <param name="StringIndex">String depicting the index value to be located</param>
/// <returns>The left n characters of the source string ending with the first occurance of a string index</returns>
public static string LeftIndexOf(string SourceString, string StringIndex) {
    string ReturnValue = SourceString;
    try {
        int EndPosition = SourceString.IndexOf(StringIndex);
        ReturnValue = SourceString.Substring(0, EndPosition);
    } catch {
    }
    return ReturnValue;
}

/// <summary>
/// Get Compacted String
/// </summary>
/// <param name="path__1"></param>
/// <param name="maxLength"></param>
/// <returns>Returns a compacted version of a very long string. (Such as a file path)</returns>
public static string GetCompactedString(string path__1, int maxLength) {
    string ellipsisChars = "...";
    char dirSeperatorChar = Path.DirectorySeparatorChar;
    string directorySeperator = dirSeperatorChar.ToString();
    if (path__1.Length <= maxLength)
        return path__1;
    int ellipsisLength = ellipsisChars.Length;
    if (maxLength <= ellipsisLength)
        return ellipsisChars;
    bool isFirstPartsTurn = true;
    string firstPart = "";
    string lastPart = "";
    int firstPartsUsed = 0;
    int lastPartsUsed = 0;
    string[] pathParts = path__1.Split(dirSeperatorChar);
    for (int i = 0; i <= pathParts.Length - 1; i++) {
        if (isFirstPartsTurn) {
            string partToAdd = pathParts[firstPartsUsed] + directorySeperator;
            if ((firstPart.Length + lastPart.Length + partToAdd.Length + ellipsisLength) > maxLength)
                break;
            firstPart = firstPart + partToAdd;
            if (partToAdd == directorySeperator) {
            } else
                isFirstPartsTurn = false;
            firstPartsUsed += 1;
        } else {
            int index = pathParts.Length - lastPartsUsed - 1;
            string partToAdd = directorySeperator + pathParts[index];
            if ((firstPart.Length + lastPart.Length + partToAdd.Length + ellipsisLength) > maxLength)
                break;
            lastPart = partToAdd + lastPart;
            if (partToAdd == directorySeperator) {
            } else
                isFirstPartsTurn = true;
            lastPartsUsed += 1;
        }
    }
    if (lastPart == "") {
        lastPart = pathParts[pathParts.Length - 1];
        lastPart = lastPart.Substring(lastPart.Length + ellipsisLength + firstPart.Length - maxLength, maxLength - ellipsisLength - firstPart.Length);
    }
    return Convert.ToString(firstPart + ellipsisChars) + lastPart;
}

/// <summary>
/// Numeric String Elements
/// </summary>
/// <param name="Elements"></param>
/// <returns>The numeric portion of a string only</returns>
public static string[] NumericStringElements(string[] Elements) {
    string[] ReturnValue = null;
    List<string> Result = new List<string>();
    try {
        foreach (var Element in Elements) {
            for (var n = 1; n <= Element.Length; n++) {
                if ("0123456789".IndexOf(Element.Substring(n, 1)) > 0) {
                    Result.Add(Element);
                    break;
                }
            }
        }
        if (Result.Count > 0)
            ReturnValue = Result.ToArray();
    } catch {
        ReturnValue = null;
    }
    return ReturnValue;
}

/// <summary>
/// Clean String - Remove nulls and other non visible characters
/// </summary>
/// <param name="InputString">String representing the input to be cleansed</param>
/// <returns>String cleanesed of all non-printable characters</returns>
public static string CleanString(string InputString) {
    string ReturnValue = null;
    try {
        ReturnValue = Encoding.ASCII.GetString(Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(Encoding.ASCII.EncodingName, new EncoderReplacementFallback(string.Empty), new DecoderExceptionFallback()), Encoding.UTF8.GetBytes(InputString)));
    } catch {
        ReturnValue = InputString;
    }
    return ReturnValue;
}

/// <summary>
/// NZ
/// </summary>
/// <param name="Value">Object to be tested</param>
/// <param name="DefaultValue">Object respresenting the default value returned if null or DBNull</param>
/// <returns> The specified default value if the input value is NULL, Zero or DBNULL (Similar to T-SQL ISNULL function)</returns>
public static object Nz(object Value, object DefaultValue = null) {
    if (Value == null || Value == DBNull.Value || Convert.ToString(Value).Length == 0) {
        return DefaultValue;
    } else {
        return Value;
    }
}

        #endregion

    } // Class

} // Namespace
