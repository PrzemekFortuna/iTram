using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DBConnection.Entities
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
