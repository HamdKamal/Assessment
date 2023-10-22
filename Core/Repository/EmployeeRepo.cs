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
                    _dbContext.Employees.Add(
                        new Employee
                        {
                            Name = obj.EmployeeName ?? "",
                            Phone = obj.Phone ?? "",
                            Email = obj.Email ?? "",
                            Image = obj.Image ?? _dataStream.ToArray(),
                            DepartmentID = obj.DepartmentID,
                            CreatedBy = userInfo.UserID.ToString(),
                        });
                    await _dbContext.SaveChangesAsync();
                    return new ResponseVM { Status = true };
                }
                catch (Exception)
                {
                    return new ResponseVM { Status = false, Msg = "Exption" };
                }
            }
            return new ResponseVM { Status = false, Msg = "Model must be not null!" };
        }

        public async Task<ResponseVM> Delete(Guid Id)
        {
            var obj = await _dbContext.Employees.Where(a=> a.ID == Id).SingleOrDefaultAsync();

            try
            {
                if (obj != null)
                {
                   obj.IsDelete = true;
                   await _dbContext.SaveChangesAsync();
                   return new ResponseVM { Status = true, Msg = "Delete Seccsfully" };
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
                        edit.Name = obj.EmployeeName ?? "";
                        edit.Phone = obj.Phone ?? "";
                        edit.Email = obj.Email ?? "";
                        edit.IsStillWorking = obj.IsStill;
                        edit.Image = obj.Image ?? _dataStream.ToArray();
                        edit.DepartmentID = obj.DepartmentID;

                        await _dbContext.SaveChangesAsync();
                        return new ResponseVM { Status = true };
                    }
                    catch (Exception)
                    {
                        return new ResponseVM { Status = false, Msg = "Exption" };
                    }
                }
            }
            return new ResponseVM { Status = false, Msg = "Model must be not null!" };
        }

        public async Task<List<EmployeeVM>> GetAll()
        {
            try
            {
                return await _dbContext.Employees.Select(a => new EmployeeVM
                    {
                      EmployeeID = a.ID,
                        EmployeeName = a.Name,
                        DepartmentID = a.DepartmentID,
                        DepartmentName = a.Department != null ? a.Department.NameEn : "",
                        Phone = a.Phone.Remove(0, 5),
                        Email = a.Email,
                        CreatedAt = a.CreatedAt,
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
                    return await _dbContext.Employees.Where(s => s.ID == Id)
                        .Select(a => new EmployeeVM
                        {
                            EmployeeID = a.ID,
                            EmployeeName = a.Name?? "",
                            DepartmentID = a.DepartmentID,
                            DepartmentName = a.Department != null ? a.Department.NameEn : "",
                            Phone = a.Phone.Remove(0, 5) ?? "",
                            Email = a.Email ?? "",
                            CreatedAt = a.CreatedAt,
                            CreatedByID = a.CreatedBy,
                            CreatedBy = a.Users != null ? a.Users.FullName : "",
                            IsStillWorking = a.IsStillWorking == true ? "Still Working" : "Not Working",
                            Color = a.IsStillWorking == true ? "green" : "red",
                        }).SingleOrDefaultAsync();
                }
                catch (Exception)
                {
                    return new EmployeeVM();
                }
            }
            return new EmployeeVM();
        }
    }
}
