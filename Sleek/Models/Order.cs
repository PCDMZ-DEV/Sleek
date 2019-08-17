#region "Usings"

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Sleek.Models {

    public partial class Order {

        #region "Table Attributes"

        [Key]
        [Display(Name = "ID")]
        public int OrdId { get; set; }

        [Display(Name = "Project")]
        [ForeignKey("Project")]
        public int? OrdProid { get; set; }

        [Display(Name = "Date")]
        public DateTime? OrdDate { get; set; }

        [Display(Name = "Subject")]
        public string OrdSubject { get; set; }

        [Display(Name = "Description")]
        public string OrdDescription { get; set; }

        [Display(Name = "Comments")]
        public string OrdComments { get; set; }

        [Display(Name = "Status")]
        [ForeignKey("Status")]
        public int? OrdStaid { get; set; }

        #endregion

        #region "Navigation Properties"

        [Display(Name = "Customer")]
        public virtual Customer Customer { get; set; }

        [Display(Name = "User")]
        public virtual User User { get; set; }

        [Display(Name = "Project")]
        public virtual Project Project { get; set; }

        [Display(Name = "Status")]
        public virtual Status Status { get; set; }

        #endregion

    }

}
