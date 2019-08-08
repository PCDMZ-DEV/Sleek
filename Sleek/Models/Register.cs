#region "Usings"

using System.ComponentModel.DataAnnotations;

#endregion

namespace Sleek.Models {

    public partial class Register {

        #region "View Attributes"

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string RegFirst { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string RegLast { get; set; }

        [Display(Name = "E-Mail Address")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string RegEmail { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        [DataType(DataType.Password)]
        public string RegPassword { get; set; }

        #endregion

    } // Class

} // Namespace
