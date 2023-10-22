using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel
{
    public class ResponseVM
    {
        public bool Status { get; set; }
        public string? Msg { get; set; }
        public object? ResultData { get; set; }
    }
}
