using Core.Interfaces;
using Core.ViewModel;
using Databases.Data;
using Databases.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<EmployeeVM>> GetEmployeeList()
        {
            var result = await _employee.GetAll();
            return result.ToArray();
        }
    }
}
