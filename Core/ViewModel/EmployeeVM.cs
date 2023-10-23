using Databases.Models.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModel
{
    public class EmployeeVM
    {

        public Guid EmployeeID { get; set; }
        public string? RefID { get; set; }

        public string? EmployeeName { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public byte[]? Image { get; set; }

        public string? IsStillWorking { get; set; } 

        public DateTime CreatedAt { get; set; } 
        public string RegisterDate { get; set; } 

        public Guid? DepartmentID { get; set; }

        public string? DepartmentName { get; set; }

        public bool IsDelete { get; set; } 
        public bool IsStill { get; set; } 

        public string? CreatedByID { get; set; }

        public string? CreatedBy { get; set; }
        public string? Color { get; set; }
        public IEnumerable<Department>? Departments { get; set; }

    }
}
