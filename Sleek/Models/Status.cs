#region "Usings"

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

        [Display(Name = "Customer")]
        public Customer Customer { get; set; }

        [Display(Name = "Project")]
        public Project Project { get; set; }

        [Display(Name = "Order")]
        public Order Order { get; set; }

        #endregion

    }

}
