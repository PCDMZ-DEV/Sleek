using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sleek.Models {

    public partial class Status {

        [Key]
        [Display(Name = "ID")]
        public int StaId { get; set; }

        [Display(Name = "Code")]
        public string StaCode { get; set; }

        [Display(Name = "Description")]
        public string StaDescription { get; set; }

        [Display(Name = "Timestamp")]
        public byte[] StaTimestamp { get; set; }

    }

}
