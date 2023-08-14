using Billboard.Data;
using Billboard.Models;
using Billboard.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Billboard.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository companyRepository;
        public CompaniesController(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        [HttpGet]
        public  IActionResult GetAll()
        {
            var data = companyRepository.GetCompanies();
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        //[HttpPost("Add")]

        //public IActionResult Add(Companies companies)
        //{
        //    var response = this.companyRepository.AddCompanies(companies);

        //    return Ok(response);
        //}

        //private readonly UserDbContext _context;
        //public CompaniesController(UserDbContext context)  // this way we will get object of DbContext at runtime
        //{
        //    _context = context;
        //}
        //[HttpGet]
        //public async Task<IEnumerable<Companies>> Get()
        //{
        //    try
        //    {
        //        return await _context.Companies.ToListAsync();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //[HttpPost("Add")]
        //public async Task<IActionResult> AddUser(Companies company)
        //{
        //    try
        //    {
        //        await _context.Companies.AddAsync(company);
        //        await _context.SaveChangesAsync();
        //        return Ok(company);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    } 
}
