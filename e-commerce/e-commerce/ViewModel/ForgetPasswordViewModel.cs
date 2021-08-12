using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.ViewModel
{
    public class ForgetPasswordViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailId { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Required]
        public string ConfirmPassword { get; set; }
        
    }
}
