using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    public class Fashion
    {
        [Key]
        public int FId { get; set; }
        [Required(ErrorMessage ="Please Enter Product Name")]
        [Display(Name ="Product Name")]
        public string FName { get; set; }
        [Required(ErrorMessage = "Please Enter Type (Men/Women)")]
        [Display(Name = "Gender")]
        public FType FType { get; set; }
        [Required(ErrorMessage = "Please Enter Category ")]
        [Display(Name = "Category")]
        public Scategory SubCategory { get; set; }
        [Required(ErrorMessage = "Please Enter Price")]
        public float Price { get; set; }
       
        
        [Required(ErrorMessage = "Please Enter Quantity")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Please Enter LaunchDate")]
        [Display(Name = "Launch Date")]
        public DateTime LaunchDate { get; set; }
        [Display(Name = "Free Delivery")]
        public bool FreeDelivery { get; set; }
        public int Rating { get; set; }
        [Display(Name = "Image")]
        public string ImageFile { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile1 { get; set; }
        public bool Active { get; set; }
        [Required(ErrorMessage = "Please Enter Brand")]
        [Display(Name = "Brand")]
        public FBrand FBrand { get; set; }
        public string Description { get; set; }
    }
    public enum FType { Men, Women }
    public enum Scategory { Watch, Wallet, Sunglasses }
    

    public enum FBrand
    {
        FastTrack, Titan, Fossil, DanielKlein
    }
}
