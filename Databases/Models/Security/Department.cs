using System.ComponentModel.DataAnnotations;

namespace Databases.Models.Security
{
    public class Department 
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();
        [Required, MaxLength(250)]
        public required string NameAr { get; set; }
        [Required, MaxLength(250)]
        public required string NameEn { get; set; }
    }
}
