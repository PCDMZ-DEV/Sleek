#region "Usings"

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Sleek.Models {

    // Activity Log (System Table)
    public partial class Activity {

        #region "Table Attributes"

        [Key]
        [Display(Name = "ID")]
        public int ActId { get; set; }

        [Display(Name = "Date")]
        public DateTime? ActDate { get; set; }

        [Display(Name = "Customer")]
        [ForeignKey("Customer")]
        public int? ActCusid { get; set; }

        [Display(Name = "User")]
        public int? ActUsrid { get; set; }

        [Display(Name = "Description")]
        public string ActDescription { get; set; }

        [Display(Name = "Type")]
        public string ActType { get; set; }

        #endregion

        #region "Navigation Properties"

        [Display(Name = "Customer")]
        public virtual Customer Customer { get; set; }

        [Display(Name = "User")]
        public virtual User User { get; set; }

        #endregion

    }

}
