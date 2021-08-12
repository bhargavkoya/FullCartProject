using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.ViewModel
{
    public class HomeDecorViewModel
    {
        
        public string HName { get; set; }
        public HType HType { get; set; }
        
        public float Price { get; set; }
        
        public int Quantity { get; set; }
        public bool Active { get; set; }

        public string Description { get; set; }
       
        public HBrand HBrand { get; set; }
        
        public string ImageFile { get; set; }
       
       
        public bool FreeDelivery { get; set; }
        
        public DateTime LaunchDate { get; set; }
        public int Rating { get; set; }
    }
}
