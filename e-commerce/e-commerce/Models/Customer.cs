using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    public class Customer
    {
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please Enter the FirstName")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please Enter the LastName")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please Enter the Email ")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter the UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter the Password")]
        [MaxLength(12)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please enter money")]
        public int Balance { get; set; }
        
    }
}
