using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.ViewModel
{
    public class OrderReviewViewModel
    {
        public DateTime Date { get; set; }
       
        public int O_Rating { get; set; }
        public status Status { get; set; }
        public string Description { get; set; }
    }
}
