
using Databases.Models;
using Databases.Models.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Databases.Data
{
    public static class ContextSeed
    {
        public enum Roles
        {
            Admin,
            Employee,
        }
        public static async Task SeedRolesAsync(UserManager<Users> userManager, RoleManager<ColumnRole> roleManager)
        {
            //Seed Roles

            var SeedAdmin = new ColumnRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = Roles.Admin.ToString(),
                NameAr = "الأدمن"
            };
            var SeedEmployee = new ColumnRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = Roles.Employee.ToString(),
                NameAr = "الموظف",
            };

            await roleManager.CreateAsync(SeedAdmin);

            await roleManager.CreateAsync(SeedEmployee);
        }
        public static async Task SeedAdminAsync(UserManager<Users> userManager, RoleManager<ColumnRole> roleManager)
        {
            //Seed Default User
            var EmployeeUser = new Users
            {
                UserName = "EMPLOYEE",
                Email = "EMPLOYEE20@gmail.com",
                EmailConfirmed = false,
                PhoneNumberConfirmed = true,
                FullName = "الموظف",
                PhoneNumber = "00966569065085",
            };
            var AdminUser = new Users
            {
                UserName = "MANAGER",
                Email = "MANAGER40@gmail.com",
                EmailConfirmed = false,
                PhoneNumberConfirmed = true,
                FullName = "الأدمن",
                PhoneNumber = "00966569065085",
                NormalizedUserName = "MANAGER",
            };

            if (userManager.Users.All(u => u.Id != AdminUser.Id))
            {
                var user = await userManager.FindByEmailAsync(AdminUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(AdminUser, "123456");
                    await userManager.AddToRoleAsync(AdminUser, Roles.Employee.ToString());
                    await userManager.AddToRoleAsync(AdminUser, Roles.Admin.ToString());
                }
            }
            if (userManager.Users.All(u => u.Id != EmployeeUser.Id))
            {
                var user = await userManager.FindByEmailAsync(EmployeeUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(EmployeeUser, "123456");
                    await userManager.AddToRoleAsync(EmployeeUser, Roles.Employee.ToString());

                }
            }
        }
    }
}
