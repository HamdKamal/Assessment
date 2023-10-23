using Databases.Models.Security;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Databases.Models
{
    public class Employee 
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();

        public string? RefID { get; set; }

        [MaxLength(250)]
        [Display(Name = "Enter Employee FullName")]
        public string? Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [Display(Name = "Enter Email Address")]
        public string? Email {  get; set; }

        [MaxLength(50)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Enter Employee Phone Number")]
        public string? Phone { get; set; }

        [Display(Name = "Please select a image smaller than 5MB")]
        public byte[]? Image { get; set; }

        public bool IsStillWorking { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid? DepartmentID { get; set; }
        [ForeignKey("DepartmentID")]
        public Department? Department { get; set; }

        public bool IsDelete { get; set; } = false;

        public string? CreatedBy { get; set; } 
        [ForeignKey("CreatedBy")]
        public Users? Users { get; set; }
    }
}
