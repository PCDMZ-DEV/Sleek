#region "Usings"

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace Sleek.Models {

    public partial class Status {

        #region Table Attributes"

        [Key]
        [Display(Name = "ID")]
        public int StaId { get; set; }

        [Display(Name = "Code")]
        public string StaCode { get; set; }

        [Display(Name = "Description")]
        public string StaDescription { get; set; }

        #endregion

        #region "Navigation Properties"

        [Display(Name = "Customers")]
        public ICollection<Customer> Customers { get; set; }

        [Display(Name = "Projects")]
        public ICollection<Project> Projects { get; set; }

        [Display(Name = "Orders")]
        public ICollection<Order> Orders { get; set; }

        [Display(Name = "Users")]
        public ICollection<User> Users { get; set; }

        #endregion

    }

}
