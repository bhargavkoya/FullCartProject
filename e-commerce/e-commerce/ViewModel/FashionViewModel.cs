using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.ViewModel
{
    public class FashionViewModel
    {
        
        public string FName { get; set; }
        
        public FType FType { get; set; }
       
        public Scategory SubCategory { get; set; }
       
        public float Price { get; set; }


        
        public int Quantity { get; set; }
       
        public DateTime LaunchDate { get; set; }
       
        public bool FreeDelivery { get; set; }
        public int Rating { get; set; }
        
        public string ImageFile { get; set; }
       
        public bool Active { get; set; }
       
        public FBrand FBrand { get; set; }
        public string Description { get; set; }
    }
}
