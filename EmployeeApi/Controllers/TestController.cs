using Core.Interfaces;
using Core.ViewModel;
using Databases.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly DatabaseDbContext _db;
        private readonly IEmployee _employee;

        public TestController(DatabaseDbContext context,IEmployee employee) 
        { 
          _db = context;
          _employee = employee;
        }

        [HttpGet]
        public async Task<List<EmployeeVM>> GetEmployeeList()
        {
            var result = await _employee.GetAll();
            return result;
        }
        [HttpGet,Route("/Test/{Emp_id}")]
        public async Task<ResponseVM> GetEmployeeById(Guid Emp_id)
        {
            var result = await _employee.GetByID(Emp_id);
            if(result != null)
            {
                return new ResponseVM
                {
                    Status = true,
                    Msg = "Employee Found",
                    ResultData = result
                };
            }
            else
            {
                return new ResponseVM
                {
                    Status = false,
                    Msg = "Employee Not Found!",
                    ResultData = null
                };
            }
           
        }
        [HttpDelete, Route("/Test/{Emp_id}")]
        public async Task<ResponseVM> DeleteEmployee(Guid Emp_id)
        {
            var result = await _employee.Delete(Emp_id);
            return result;
        }
        [HttpPost]
        public async Task<ResponseVM> AddNewEmployee(EmployeeVM obj)
        {
            var UserInfo = new UserInfoVM
            {
                UserID = Guid.Parse("b9a1ca1c-b0a6-4ecb-9f69-1dd6caf6f249")
            };
            var result = await _employee.Add(obj,UserInfo);
            return result;
        }
        [HttpPut]
        public async Task<ResponseVM> EditEmployee(EmployeeVM obj)
        {
            var UserInfo = new UserInfoVM
            {
                UserID = Guid.Parse("b9a1ca1c-b0a6-4ecb-9f69-1dd6caf6f249")
            };
            var result = await _employee.Edit(obj, UserInfo);
            return result;
        }
    }
}
