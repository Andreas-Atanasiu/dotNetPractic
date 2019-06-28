using dotNetLabPractic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetLabPractic.DTOs
{
    public class GetUserDto
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

        [EnumDataType(typeof(UserRole))]
        public UserRole UserRole { get; set; }

    }
}
