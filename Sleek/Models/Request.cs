#region "Usings"

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace Sleek.Models {

    public partial class Request {

        #region "Table Attributes"

        [Key]
        [Display(Name = "ID")]
        public int ReqId { get; set; }

        [Display(Name = "Date")]
        public DateTime? ReqDate { get; set; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string ReqType { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        [MinLength(2, ErrorMessage = "{0} must be at least 2 characters")]
        public string ReqFirst { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        [MinLength(2, ErrorMessage = "{0} must be at least 2 characters")]
        public string ReqLast { get; set; }

        [Display(Name = "Address")]
        [MinLength(6, ErrorMessage = "{0} must be at least 6 characters")]
        public string ReqAddress { get; set; }

        [Display(Name = "City")]
        [MinLength(3, ErrorMessage = "{0} must be at least 3 characters")]
        public string ReqCity { get; set; }

        [Display(Name = "State")]
        [MinLength(2, ErrorMessage = "{0} must be at least 2 characters")]
        public string ReqState { get; set; }

        [Display(Name = "Zip")]
        [MinLength(2, ErrorMessage = "{0} must be at least 2 characters")]
        public string ReqZip { get; set; }

        [Display(Name = "Phone")]
        [Phone(ErrorMessage = "Please provide a valid {0}")]
        public string ReqPhone { get; set; }

        [Display(Name = "E-Mail Address")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        [EmailAddress(ErrorMessage = "Please provide a valid {0}")]
        public string ReqEmail { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string ReqSubject { get; set; }

        [Display(Name = "Message")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string ReqContent { get; set; }

        #endregion

    }

}
