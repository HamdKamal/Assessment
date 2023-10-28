using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel
{
    public class TokenVM
    {
        public string? UserID { get; set; }
        public string? UserName { get; set; }
        public bool IsAuthenticated { get; set; }
        public string? Message { get; set; }
        public string? Token { get; set; }
    }
}
