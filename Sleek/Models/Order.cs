using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sleek.Models {

    public partial class Order {

        [Key]
        [Display(Name = "ID")]
        public int OrdId { get; set; }

        [Display(Name = "Customer")]
        public int OrdCusid { get; set; }

        [Display(Name = "User")]
        public int OrdUsrid { get; set; }

        [Display(Name = "Project")]
        public int OrdProid { get; set; }

        [Display(Name = "Date")]
        public DateTime? OrdDate { get; set; }

        [Display(Name = "Subject")]
        public string OrdSubject { get; set; }

        [Display(Name = "Comments")]
        public string OrdComments { get; set; }

        [Display(Name = "Status")]
        public int OrdStaid { get; set; }

    }

}
