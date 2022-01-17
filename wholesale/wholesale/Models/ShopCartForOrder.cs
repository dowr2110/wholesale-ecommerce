using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wholesale.Models
{
    public class ShopCartForOrder
    {
        [Key]
        public int Id { get; set; }

        public int Count { get; set; }


        public int Sum { get; set; }

        [Required]
        [ForeignKey("Product")]
        public virtual int ProductId { get; set; }
        public virtual Product Product { get; set; }

  
        public  int Id_User { get; set; }
       
    }
}