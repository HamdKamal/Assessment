using Core.Services;
using Databases.Models.Databases.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly SeedingService _seedingService;
        public SeedController(SeedingService seedingService)
        {
            _seedingService = seedingService;
        }

        [HttpPost("from-json")]
        public async Task<IActionResult> SeedFromJson([FromBody] TBL_Primary primaryData)
        {
            if (primaryData == null)
            {
                return BadRequest("Invalid JSON data.");
            }

            await _seedingService.SeedFromJsonAsync(primaryData);
            return Ok("Database seeded successfully.");
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _seedingService.GetAllAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var item = await _seedingService.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }        

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TBL_Primary primary)
        {
            _seedingService.Update(primary);
            await _seedingService.SaveAsync();
            return Ok("Updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var entity = await _seedingService.GetByIdAsync(id);
            if (entity == null) return NotFound();

            _seedingService.Delete(entity);
            await _seedingService.SaveAsync();
            return Ok("Deleted successfully");
        }

    }
}
