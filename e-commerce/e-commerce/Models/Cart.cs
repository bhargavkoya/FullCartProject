using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public int Userid { get; set; }
        [ForeignKey("Userid")]
        public Customer Customer { get; set; }
        [Required(ErrorMessage ="Please enter Product Name") ]
        [Display(Name ="Product Name")]
        public string productName { get; set; }
        [Required(ErrorMessage = "Please enter Price")]
        public float Price { get; set; }
        [Required(ErrorMessage = "Please enter Quantity")]
        public int Quantity { get; set; }
        public string productImage { get; set; }
        [Required(ErrorMessage = "Please enter Description")]
        public string Description { get; set; }
        public bool FreeDelivery { get; set; }
        [Required(ErrorMessage = "Please enter Category")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Please enter Brand")]
        public string Brand { get; set; }
        
    }
}
