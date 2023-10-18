using Databases.Models.Security;

namespace Core.ViewModel
{
    public class UserInfo
    {
        public Guid UserID { get; set; }
        public string? UserName { get; set; }
        public string? RoleName { get; set; }
        public Guid? DepartmentID { get; set; }
        public Department? Department { get; set; }
        public Guid? RoleID { get; set; }
        public Roles? Roles { get; set; }

        public List<Guid?>? UserRoles { get; set; }
    }
}
