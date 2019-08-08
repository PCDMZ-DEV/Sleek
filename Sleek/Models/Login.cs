#region "Usings"

using System.ComponentModel.DataAnnotations;

#endregion

namespace Sleek.Models {

    public partial class Login {

        #region "View Attributes"

        [Display(Name = "E-Mail Address")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        [EmailAddress(ErrorMessage = "Please supply a valid {0}")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string Rememberme { get; set; }

        #endregion

    } // Class

} // Namespace
