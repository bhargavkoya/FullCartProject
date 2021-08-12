using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceAPI.Models
{
    public class ElectronicDevice
    {
        [Key]
        public int EId { get; set; }
        [Required(ErrorMessage ="Please Enter Product Name")]
        [Display(Name ="Product Name")]
        public string EName { get; set; }
        public Category Category { get; set; }
        [Required(ErrorMessage = "Please Enter Price")]
        public float Price { get; set; }
        [Required(ErrorMessage = "Please Enter  Quantity")]
        public int Quantity { get; set; }
        public int Rating { get; set; }
        [Required(ErrorMessage = "Please Enter Launch Date")]
        [Display(Name ="Launch Date")]
        public DateTime LaunchDate { get; set; }
        [Display(Name = "Image")]
        public string ImageFile { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile1 { get; set; }

        [Required(ErrorMessage = "Please Enter Brand")]
        [Display(Name = "Brand")]
        public EBrand EBrand { get; set; }
        public string Description { get; set; }
        [Display(Name = "Free Delivery")]
        public Boolean  FreeDelivery { get; set; }
        public Boolean Active { get; set; }
    }

    public enum Category
    {
        Laptop, Mobile, EarPhone, Camera, Television, Printers
    }

    public enum EBrand
    { 
        Bajaj, Sony, Lenovo, HP, RedMI, APPLE, OnePlus, Panasonic, Toshiba,Dell, Samsung, Intel, Microsoft, IBM
    }
}
