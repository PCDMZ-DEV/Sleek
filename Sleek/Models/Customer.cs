using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sleek.Models {

    public partial class Customer {

        [Key]
        [Display(Name = "ID")]
        public int CusId { get; set; }

        [Display(Name = "Reference")]
        public string CusNumber { get; set; }

        [Display(Name = "Company")]
        public string CusCompany { get; set; }

        [Display(Name = "First")]
        public string CusFirst { get; set; }

        [Display(Name = "Last")]
        public string CusLast { get; set; }

        [Display(Name = "Address")]
        public string CusAddress1 { get; set; }

        [Display(Name = "Address")]
        public string CusAddress2 { get; set; }

        [Display(Name = "City")]
        public string CusCity { get; set; }

        [Display(Name = "State")]
        public string CusState { get; set; }

        [Display(Name = "Zip")]
        public string CusZip { get; set; }

        [Display(Name = "ID")]
        public string CusZip4 { get; set; }

        [Display(Name = "Phone")]
        public string CusPhone { get; set; }

        [Display(Name = "Fax")]
        public string CusFax { get; set; }

        [Display(Name = "E-Mail")]
        public string CusEmail { get; set; }

        [Display(Name = "Password")]
        public string CusPassword { get; set; }

        [Display(Name = "Note")]
        public string CusNote { get; set; }

        [Display(Name = "ID")]
        public int? CusStaid { get; set; }

    }

}
