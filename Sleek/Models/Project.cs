using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sleek.Models {

    public partial class Project {

        [Key]
        [Display(Name = "ID")]
        public int ProId { get; set; }

        [Display(Name = "Customer")]
        public int ProCusid { get; set; }

        [Display(Name = "User")]
        public int ProUsrid { get; set; }

        [Display(Name = "Date")]
        public DateTime? ProDate { get; set; }

        [Display(Name = "Description")]
        public string ProDescription { get; set; }

        [Display(Name = "Local Path")]
        public string ProLocalpath { get; set; }

        [Display(Name = "Remote Path")]
        public string ProRemotepath { get; set; }

        [Display(Name = "Source Path")]
        public string ProSourcepath { get; set; }

        [Display(Name = "Status")]
        public int ProStaid { get; set; }

    }

}
