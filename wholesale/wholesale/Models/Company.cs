using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wholesale.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        public string Company_name { get; set; }
   
    }
}