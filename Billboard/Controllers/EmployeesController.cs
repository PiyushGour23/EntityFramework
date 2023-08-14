using Billboard.Data;
using Billboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Billboard.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly UserDbContext _context;
        public EmployeesController(UserDbContext context)  // this way we will get object of DbContext at runtime
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IEnumerable<Employees>> GetEmployees()
        {
            try
            {
                return await _context.Employees.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddUser(Employees employees)
        {
            try
            {
                await _context.Employees.AddAsync(employees);
                await _context.SaveChangesAsync();
                return Ok(employees);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
