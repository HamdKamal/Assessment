using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.ViewModel
{
    public class LoginVM
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Message { get; set; }
    }
}
