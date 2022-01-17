using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace wholesale.Models
{
    public class ContactMessage
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string Username { get; set; }

        public string Subject { get; set; }

        public string Date { get; set; }

        public string Email { get; set; }


    }
}