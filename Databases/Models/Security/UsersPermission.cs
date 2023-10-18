using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Databases.Models.Security
{
    public class UsersPermission
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();
        public required string RoleID { get; set; }
    }
}
