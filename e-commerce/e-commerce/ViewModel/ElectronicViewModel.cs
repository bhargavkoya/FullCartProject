using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.ViewModel
{
    public class ElectronicViewModel
    {
       public string EName { get; set; }
        public Category Category { get; set; }
        
        public float Price { get; set; }
        
        public int Quantity { get; set; }
        public int Rating { get; set; }
       
        public DateTime LaunchDate { get; set; }
       
        public string ImageFile { get; set; }
       
        public EBrand EBrand { get; set; }
        public string Description { get; set; }
       
        public Boolean FreeDelivery { get; set; }
        public Boolean Active { get; set; }
    }
}
