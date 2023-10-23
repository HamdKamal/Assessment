using Azure;
using Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IEmployee
    {
        Task<ResponseVM> Add(EmployeeVM obj, UserInfoVM userInfo);
        Task<ResponseVM> Edit(EmployeeVM obj, UserInfoVM userInfo);
        Task<ResponseVM> Delete(Guid Id);
        Task<List<EmployeeVM>> GetAll();
        Task<EmployeeVM> GetByID(Guid Id);
        string GetRefrence();
    }
}
