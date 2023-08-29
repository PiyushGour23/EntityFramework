using Billboard.Data;
using Billboard.Models;
using Billboard.Service;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Billboard.Controllers
{
    //[Authorize]
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

        [HttpGet("id")]
        public async Task<IActionResult> GetId(int id)
        {
            var data = companyRepository.GetById(id);
            return Ok(data);

        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(Companies companies)
        {
            await companyRepository.AddCompanies(companies);
            return Ok();
        }

        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove(int id)
        {
            await companyRepository.DeleteCompanies(id);
            return Ok();
        }
    } 
}
