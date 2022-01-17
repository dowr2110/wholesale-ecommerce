using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wholesale.Models
{
    public class Wishlist
    {
        public int Id { get; set; }

      

        [Required]
        [ForeignKey("Product")]
        public virtual int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Required]
        [ForeignKey("User")]
        public virtual int UserId { get; set; }
        public virtual User User { get; set; }
    }
}