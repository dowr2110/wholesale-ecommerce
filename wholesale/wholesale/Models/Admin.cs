using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace wholesale.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        public string Admin_name { get; set; }
        public string Password { get; set; }
    }
}