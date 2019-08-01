#region "Imported Namespaces"

using System.ComponentModel.DataAnnotations;

#endregion

namespace Sleek.Models {

    public partial class Login {

        [Display(Name = "E-Mail Address")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        [EmailAddress(ErrorMessage = "Please supply a valid {0}")]
        public string LgnEmail { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string LgnPassword { get; set; }

    } // Class

} // Namespace
