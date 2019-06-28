using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetLabPractic.DTOs
{
    public class PostUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
