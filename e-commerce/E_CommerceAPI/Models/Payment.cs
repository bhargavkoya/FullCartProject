using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceAPI.Models
{
    public class Payment
    {
        [Key]
        public int PId { get; set; }
        [DataType(DataType.CreditCard)]
        [Required(ErrorMessage ="Please Enter Card Number")]
        public long CardNumber { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Enter Card Holder Name")]
        public string CardHolderName { get; set; }
        public double Balance { get; set; }

    }
}
