using Assessment.Controllers;
using Core.Interfaces;
using Core.ViewModel;
using Databases.Data;
using Localization.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.IO;
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
        private List<string> _allowedExtenstions = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576;
        private readonly ILocalizedService _myLocalizedService;

        public EmployeeController(DatabaseDbContext dbContext,IToastNotification toast,IEmployee employee, ILocalizedService myLocalizedService) 
        {
            _db = dbContext;
            _toastNotification = toast;
            _employee = employee;
            _myLocalizedService = myLocalizedService;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            string message = _myLocalizedService.GetLocalizedMessage();
            var _Object = await _employee.GetAll(GV_Lang);
            _Object.ForEach(a => a.Localizer = message);
            return View(_Object);
        }
        [Authorize]
        public async Task<IActionResult> Create(Guid? id)
        {
            var Result = await _employee.GetByID(id ?? new Guid()) ?? new EmployeeVM { EmployeeID = Guid.Empty };
            if (id != Guid.Empty)
            {
                Result.Departments = await _db.Departments.OrderBy(m => m.ID).ToListAsync();
            }
            else 
            {
                Result.RefID = _employee.GetRefrence();
                Result.Departments = await _db.Departments.OrderBy(m => m.ID).ToListAsync();
            }
            return View(Result);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM _EmpObject)
        {
            
            if (_EmpObject != null)
            {
                var files = Request.Form.Files;

                if (!files.Any())
                {
                    _EmpObject.Departments = await _db.Departments.OrderBy(m => m.ID).ToListAsync();
                    ModelState.AddModelError("Image", "Please select Employee Image!");
                    return View("Create", _EmpObject);
                }

                var poster = files.FirstOrDefault();

                if (!_allowedExtenstions.Contains(Path.GetExtension(poster.FileName).ToLower()))
                {
                    _EmpObject.Departments = await _db.Departments.OrderBy(m => m.ID).ToListAsync();
                    ModelState.AddModelError("Image", "Please select Employee Image!");
                    return View("Create", _EmpObject);
                }

                if (poster.Length > _maxAllowedPosterSize)
                {
                    _EmpObject.Departments = await _db.Departments.OrderBy(m => m.ID).ToListAsync();
                    ModelState.AddModelError("Image", "Please select Employee Image!");
                    return View("Create", _EmpObject);
                }

                using var dataStream = new MemoryStream();
                await poster.CopyToAsync(dataStream);
                _EmpObject.Image = dataStream.ToArray();

                var Result = _EmpObject.EmployeeID == Guid.Empty ? await _employee.Add(_EmpObject, UserInfo) : await _employee.Edit(_EmpObject, UserInfo); 
                if (Result.Status)
                {
                    if(Result.Msg == "Add")
                    {
                        _toastNotification.AddSuccessToastMessage("Employee Created successfully");
                    }
                    else
                    {
                        _toastNotification.AddSuccessToastMessage("Employee Edited successfully");
                    }
                    return RedirectToAction("Index", "Employee", new { area = "Employee" });
                }
                else
                {
                    return View(_EmpObject);
                }
            }
            else
            {
                var Result = await _employee.GetByID(_EmpObject.EmployeeID);
                Result.Departments = await _db.Departments.OrderBy(m => m.ID).ToListAsync();
                return View(Result);
            }
        }
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Details(Guid id)
        {
            var Result = await _employee.GetByID(id);
            return View(Result);
        }
    }
}
