using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Register
{
    public class NewUserDto
    {

        public string Username { get; set; }

        public string Email { get; set; }
        public string Tokem { get; set; }

    }
}