using Assessment.Controllers;
using Core.Interfaces;
using Core.ViewModel;
using Databases.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System;
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
        private readonly IEmployee _employee;
        //private new List<string> _allowedExtenstions = new List<string> { ".jpg", ".png" };

        public EmployeeController(DatabaseDbContext dbContext,IToastNotification toast,IEmployee employee) 
        {
            _db = dbContext;
            _toastNotification = toast;
            _employee = employee;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _employee.GetAll());
        }
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var Result = await _employee.Delete(id);
            if (Result.Status)
            {
                return Json(new { data = "1" });
            }
            else
            {
                return Json(new { data = "-1" });
            }
        }
    }
}
