using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wholesale.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
  
        public int Sum { get; set; }
        public string Date { get; set; }

        public string Telno { get; set; }
        public string Cartno { get; set; }
        public string Company { get; set; }
        public string Add1 { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Bank { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }

        /*[Required]
        [ForeignKey("ShopCartForOrder")]
        public virtual int ShopCard2_id { get; set; }
        public virtual ShopCartForOrder ShopCartForOrder { get; set; }*/

        [Required]
        [ForeignKey("User")]
        public virtual int UserId { get; set; }
        public virtual User User { get; set; }
    }   
}