using Databases.Models;
using Databases.Models.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Databases.Data
{
    public class DatabaseDbContext : IdentityDbContext<Users>
    {
        public DatabaseDbContext()
        { }
        public DatabaseDbContext(DbContextOptions<DatabaseDbContext> options)
            : base(options)
        {
        }
        public DbSet<UsersPermission> UsersPermissions { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ColumnRole> ColumnRoles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            base.OnConfiguring(option);
            if (!option.IsConfigured)
            {
                
                option.UseSqlServer("Data Source = Mo-Kamal\\SQLEXPRESS; Initial Catalog = AssessmentDB; Integrated Security = True; MultipleActiveResultSets=True;TrustServerCertificate=True");
            }
            option.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>(entity => { entity.ToTable(name: "sys_Users"); });
            modelBuilder.Entity<ColumnRole>(entity => { entity.ToTable(name: "sys_Roles"); });
            modelBuilder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("sys_UserRoles"); });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("sys_UserClaims"); });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("sys_UserLogins"); });
            modelBuilder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("sys_UserTokens"); });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("sys_RoleClaims"); });


            //modelBuilder.Entity<Employee>().Ignore(x => x.Image);



            modelBuilder.Entity<Department>().HasData(
             new Department
             {
                 ID = new Guid("98d053d0-ab0d-4510-ac10-32d29189a301"),
                 NameAr = "إدارة شؤون الموظفين",
                 NameEn = "HR Department"
             },
             new Department
             {
                ID = new Guid("98d053d0-ab0d-4510-ac10-32d29189a302"),
                 NameAr = "الإدارة المالية",
                 NameEn = "financial Department "
             });
        }
    }
}
