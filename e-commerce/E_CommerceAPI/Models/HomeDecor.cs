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
    public class HomeDecor
    {
        [Key]
        public int HId { get; set; }
        [Required(ErrorMessage ="Please Enter Product Name")]
        [Display(Name ="Product Name")]
        public string HName { get; set; }
        public HType HType { get; set; }
        [Required(ErrorMessage = "Please Enter Price")]
        public float Price { get; set; }
        [Required(ErrorMessage = "Please Enter Quantity")]
        public int Quantity { get; set; }
        public bool Active { get; set; }
       
        public string Description { get; set; }
        [Required(ErrorMessage = "Please select Brand")]
        [Display(Name = "Brand")]
        public HBrand HBrand { get; set; }
        [Display(Name = "Image")]
        public string ImageFile { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile1 { get; set; }
        [Display(Name = "Free Delivery")]
        public bool FreeDelivery { get; set; }
        [Display(Name = "Launch Date")]
        public DateTime LaunchDate { get; set; }
        public int Rating { get; set; }
    }
    public enum HType
    {
        Furniture, SecurityCameras, SmartHomelightening, Clocks, Mirrors, Wallpapers, DreamCatcher
    }
    public enum HBrand
    {
        Pepperfry, UrbanLadder, HomeTown, HomeCenter, @Home, Damro

    }

}
