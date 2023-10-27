using Core.Interfaces;
using Core.ViewModel;
using Databases.Data;
using Databases.Models;
using Databases.Models.Security;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Repository
{
    public class EmployeeRepo : IEmployee
    {
        private readonly DatabaseDbContext _dbContext;

        public EmployeeRepo(DatabaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<ResponseVM> Add(EmployeeVM obj, UserInfoVM userInfo)
        {
            var _dataStream = new MemoryStream();
            if (obj != null)
            {
                try
                {
                    var _Object = new Employee
                    {
                        RefID = GetRefrence(),
                        Name = obj.EmployeeName,
                        Phone = "00966" + obj.Phone,
                        Email = obj.Email,
                        Image = obj.Image,
                        DepartmentID = obj.DepartmentID,
                        CreatedBy = userInfo.UserID.ToString()
                    };

                    await _dbContext.Employees.AddAsync(_Object);
                    await _dbContext.SaveChangesAsync();
                    return new ResponseVM { Status = true , Msg = "Add",ResultData = _Object };
                }
                catch (Exception)
                {
                    return new ResponseVM { Status = false, Msg = "Exption" };
                }
            }
            return new ResponseVM { Status = false, Msg = "Model must be not null!", ResultData = obj };
        }

        public async Task<ResponseVM> Delete(Guid Id)
        {
            try
            {
                var obj = await _dbContext.Employees.FindAsync(Id);
                
                if (obj != null)
                {
                    if (obj.IsDelete == true)
                    {
                        return new ResponseVM { Status = false, Msg = "Employee Deleted Before!" };
                    }
                    obj.IsDelete = true;
                   await _dbContext.SaveChangesAsync();
                   return new ResponseVM { Status = true, Msg = "Delete Seccsfully",ResultData = obj };
                }
            }
            catch (Exception)
            {
                return new ResponseVM { Status = false, Msg = "Exption" };
            }
            return new ResponseVM { Status = false, Msg = "Id must be not null!" };
        }

        public async Task<ResponseVM> Edit(EmployeeVM obj, UserInfoVM userInfo)
        {
            var _dataStream = new MemoryStream();
            if (obj.EmployeeID != Guid.Empty)
            {
                var edit = await _dbContext.Employees.FindAsync(obj.EmployeeID);
                if (edit != null)
                {
                    try
                    {
                        edit.Name = obj.EmployeeName;
                        edit.Phone = "00966" + obj.Phone;
                        edit.Email = obj.Email;
                        edit.IsStillWorking = obj.IsStill;
                        edit.Image = obj.Image;
                        edit.DepartmentID = obj.DepartmentID;

                        await _dbContext.SaveChangesAsync();
                        return new ResponseVM { Status = true , Msg = "Edit", ResultData = edit };
                    }
                    catch (Exception)
                    {
                        return new ResponseVM { Status = false, Msg = "Exption"};
                    }
                }
                else
                {
                    return new ResponseVM { Status = false, Msg = "Employee Id Not Found!" };
                }
            }
            return new ResponseVM { Status = false, Msg = "Model must not be null!"};
        }

        public async Task<List<EmployeeVM>> GetAll()
        {
            try
            {
                return await _dbContext.Employees.Where(e=> e.IsDelete != true).OrderBy(e => e.RefID)
                    .Select(a => new EmployeeVM
                    {
                        EmployeeID = a.ID,
                        RefID = a.RefID,
                        EmployeeName = a.Name,
                        DepartmentID = a.DepartmentID,
                        DepartmentName = a.Department != null ? a.Department.NameEn : "",
                        Phone = a.Phone.Remove(0, 5),
                        Email = a.Email,
                        Image = a.Image,
                        CreatedAt = a.CreatedAt,
                        RegisterDate = a.CreatedAt.ToShortDateString(),
                        CreatedByID = a.CreatedBy,
                        CreatedBy = a.Users != null ? a.Users.FullName : "",
                        IsStillWorking = a.IsStillWorking == true ? "Still Working" : "Not Working",
                        Color = a.IsStillWorking == true ? "green" : "red",
                    }).ToListAsync();
            }
            catch (Exception)
            {
                return new List<EmployeeVM>();
            }
        }

        public async Task<EmployeeVM> GetByID(Guid Id)
        {
            if(Id != Guid.Empty)
            {
                try
                {
                    var Obj = await _dbContext.Employees.Where(e => e.IsDelete != true && e.ID == Id).Select(a => new EmployeeVM
                    {
                        EmployeeID = a.ID,
                        RefID = a.RefID,
                        EmployeeName = a.Name,
                        DepartmentID = a.DepartmentID,
                        DepartmentName = a.Department != null ? a.Department.NameEn : "",
                        Phone = a.Phone.Remove(0, 5),
                        Email = a.Email,
                        Image = a.Image,
                        CreatedAt = a.CreatedAt,
                        RegisterDate = a.CreatedAt.ToShortDateString(),
                        CreatedByID = a.CreatedBy,
                        CreatedBy = a.Users != null ? a.Users.FullName : "",
                        IsStill = a.IsStillWorking,
                        IsStillWorking = a.IsStillWorking == true ? "Still Working" : "Not Working",
                        Color = a.IsStillWorking == true ? "green" : "red",
                    }).SingleOrDefaultAsync();
                    return Obj;
                }
                catch (Exception)
                {
                    return new EmployeeVM();
                }
            }
            return new EmployeeVM();
        }

        public string GetRefrence()
        {
            string RefID;
            var GetCount = _dbContext.Employees.ToList();
            if (GetCount.Count == 0)
            {
                RefID = "Emp-001";
            }
            else
            {
                var _NewCount = GetCount.Count + 1;
                RefID = "Emp-00" + _NewCount;
            }
            return RefID;
        }
    }
}
