using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wholesale.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Prod_name { get; set; }
        public int Price { get; set; }

        

        [Required]
        [ForeignKey("Company")]
        public virtual int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        [Required]
        [ForeignKey("Category")]
        public virtual int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public string Discription { get; set; }


    }
}