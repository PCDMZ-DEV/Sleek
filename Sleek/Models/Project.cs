#region "Usings"

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Sleek.Models {

    public partial class Project {

        #region "Table Attributes"

        [Key]
        [Display(Name = "ID")]
        public int ProId { get; set; }

        [Display(Name = "Customer")]
        [ForeignKey("Customer")]
        public int? ProCusid { get; set; }

        [Display(Name = "User")]
        [ForeignKey("User")]
        public int? ProUsrid { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public DateTime? ProDate { get; set; }

        [Display(Name = "Description")]
        [StringLength(300)]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string ProDescription { get; set; }

        [Display(Name = "Local Path")]
        [StringLength(300)]
        public string ProLocalpath { get; set; }

        [Display(Name = "Remote Path")]
        [StringLength(300)]
        public string ProRemotepath { get; set; }

        [Display(Name = "Source Path")]
        [StringLength(300)]
        public string ProSourcepath { get; set; }

        [Display(Name = "Status")]
        [ForeignKey("Status")]
        public int? ProStaid { get; set; }

        #endregion

        #region "Navigation Properties"

        [Display(Name = "Customer")]
        public virtual Customer Customer { get; set; }

        [Display(Name = "User")]
        public virtual User User { get; set; }

        [Display(Name = "Status")]
        public virtual Status Status { get; set; }

        [Display(Name = "Orders")]
        public ICollection<Order> Orders { get; set; }

        #endregion

    }

}
