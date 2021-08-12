using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    public class OrderReview
    {
        [Key]
        public int Id { get; set; }
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public Customer Customer { get; set; }
        [Required(ErrorMessage ="Please Enter Today's Date")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Please Enter Rating From 1 to 5")]
        public int O_Rating { get; set; }
        public status Status { get; set; }

        public string Description { get; set; }
        public int OId { get; set; }
       
    }
    public enum status { Successed, Failed }

}
