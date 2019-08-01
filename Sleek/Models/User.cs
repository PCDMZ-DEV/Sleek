using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sleek.Models {

    public partial class User {

        [Key]
        [Display(Name = "ID")]
        public int UsrId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string UsrFirst { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string UsrLast { get; set; }

        [Display(Name = "Title")]
        public string UsrTitle { get; set; }

        [Display(Name = "User Name")]
        public string UsrName { get; set; }

        [Display(Name = "E-Mail Address")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string UsrEmail { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        [DataType(DataType.Password)]
        public string UsrPassword { get; set; }

        [Display(Name = "Note")]
        public string UsrNote { get; set; }

        [Display(Name = "Role Assignment")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string UsrRole { get; set; }

        [Display(Name = "Token")]
        public string UsrToken { get; set; }

        [Display(Name = "Token Date")]
        public DateTime? UsrTokendate { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public int? UsrStaid { get; set; }

        [Display(Name = "Timestamp")]
        public byte[] UsrTimestamp { get; set; }

    }

}
