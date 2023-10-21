using Assessment.Controllers;
using Core.ViewModel;
using Databases.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize]
    [Route("Employee/{controller}/{action=Index}/{id?}")]
    public class EmployeeController : BaseController
    {
        private readonly DatabaseDbContext _db;
        private readonly IToastNotification _toastNotification;
        private new List<string> _allowedExtenstions = new List<string> { ".jpg", ".png" };

        public EmployeeController(DatabaseDbContext dbContext,IToastNotification toast) 
        {
            _db = dbContext;
            _toastNotification = toast;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _db.Employees.Select(a=> new EmployeeVM
            {
                EmployeeID = a.ID,
                EmployeeName = a.Name,
                DepartmentID = a.DepartmentID,
                DepartmentName = a.Department.NameEn,
                Phone = a.Phone,
                Email = a.Email,
                CreatedAt = a.CreatedAt,
                CreatedByID = a.CreatedBy,
                CreatedBy = a.Users.UserName,
                IsStillWorking = a.IsStillWorking,
                Color = a.IsStillWorking == true ? "green" : "red",
            }).ToListAsync();
            return View(result);
        }
    }
}
