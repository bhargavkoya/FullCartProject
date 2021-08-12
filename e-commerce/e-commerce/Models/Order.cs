using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace e_commerce.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public float Price { get; set; }
        public int AddrId { get; set; }
        [ForeignKey("AddrId")]
        public Address Address { get; set; }
        public DateTime DateOfOrder { get; set; }
        public paymentmode PaymentMode { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime DateOfDelivery { get; set; }
        
    }
    public enum paymentmode { CashOnDelivery, creditCard }
    public enum OrderStatus
    {
        Progress = 1,
        OnShipping = 2,
        Finished = 3
    }

}
