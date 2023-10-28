using Databases.Models.Security;

namespace Core.ViewModel
{
    public class UserInfoVM
    {
        public Guid UserID { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? RoleName { get; set; }
        public Guid? DepartmentID { get; set; }
        public Department? Department { get; set; }
        public Guid? RoleID { get; set; }
        public RolesVM? Roles { get; set; }
        public List<Guid?>? UserRoles { get; set; }
        public string? Token { get; set; }
        public bool IsAuthenticated { get; set; }
        public string? Message { get; set; }
    }
}
