using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceAPI.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        [Required(ErrorMessage = "Please enter first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter last name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter mail")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Please enter phone number")]
        public string PhoneNo { get; set; }
        [Required(ErrorMessage = "Please enter address line one")]
        [Display(Name ="Address Line 1")]
        public string AddressLineone { get; set; }
        [Display(Name = "Address Line 2")]
        public string AddressLinetwo { get; set; }
        [Required(ErrorMessage = "Please enter country")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Please enter city")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please enter state")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please enter pincode")]

        public string PinCode { get; set; }

        public int CustId { get; set; }
        [ForeignKey("CustId")]
        public Customer Customer { get; set; }


    }
}
