#region "Usings"

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#endregion

namespace Sleek.Models {

    public partial class Register {

        #region Table Attributes"

        [Display(Name = "Company Name")]
        [Required(ErrorMessage ="{0} cannot be blank")]
        public string CusCompany { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string CusFirst { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string CusLast { get; set; }

        [Display(Name = "Address Line 1")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string CusAddress1 { get; set; }

        [Display(Name = "Address Line 2")]
        public string CusAddress2 { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string CusCity { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string CusState { get; set; }

        [Display(Name = "Zip")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string CusZip { get; set; }

        [Display(Name = "Telephone Number")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string CusPhone { get; set; }

        [Display(Name = "E-Mail Address")]
        [Required(ErrorMessage = "{0} cannot be blank")]
        public string CusEmail { get; set; }

        #endregion

    }

}
