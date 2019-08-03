using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sleek.Models {
    public partial class User {

        [Key]
        [Display(Name = "ID")]
        public int UsrId { get; set; }

        [Display(Name = "Customer")]
        public int UsrCusid { get; set; }

        [Display(Name = "First")]
        public string UsrFirst { get; set; }

        [Display(Name = "Last")]
        public string UsrLast { get; set; }

        [Display(Name = "Title")]
        public string UsrTitle { get; set; }

        [Display(Name = "User")]
        public string UsrName { get; set; }

        [Display(Name = "E-Mail")]
        public string UsrEmail { get; set; }

        [Display(Name = "Password")]
        public string UsrPassword { get; set; }

        [Display(Name = "Note")]
        public string UsrNote { get; set; }

        [Display(Name = "Role")]
        public string UsrRole { get; set; }

        [Display(Name = "Token")]
        public string UsrToken { get; set; }

        [Display(Name = "Token Date")]
        public DateTime? UsrTokendate { get; set; }

        [Display(Name = "Status")]
        public int? UsrStaid { get; set; }

    }

}
