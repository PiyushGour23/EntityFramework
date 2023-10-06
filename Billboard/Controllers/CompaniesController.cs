using AutoMapper;
using Billboard.Data;
using Billboard.Models;
using Billboard.Models.DTO;
using Billboard.Service;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;

namespace Billboard.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository companyRepository;
        private readonly UserDbContext _context;
        private readonly IMapper _mapper;
        public CompaniesController(ICompanyRepository companyRepository, UserDbContext context, IMapper mapper)
        {
            this.companyRepository = companyRepository;
            _context = context;
            _mapper = mapper;
        }



        //https://localhost:44362/api/Companies
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await companyRepository.GetCompanies();
            if (data == null)
            {
                return NotFound();
            }

            //Map Domain Model to DTO
            var resposne = new List<CompaniesAngularDto>();
            foreach (var company in data)
            {
                resposne.Add(new CompaniesAngularDto
                {
                    Id = company.Id,
                    companyId = company.companyId,
                    CompanyName = company.CompanyName
                });
            }
            return Ok(resposne);
            //return _mapper.Map<List<CompaniesDto>>(data);
        }

        
        [HttpGet("id")]
        public async Task<IActionResult> GetId(int id)
        {
            var data = await companyRepository.GetById(id);
            return Ok(data);

        }

        [HttpGet("companyId")]
        public async Task<IActionResult> GetById(int companyId)
        {
            try
            {
                var data = await companyRepository.GetByCompanyId(companyId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CompaniesDto companiesDto)
        {

            try
            {
                //Map DTO to Domain Model
                var companiesdto = new Companies
                {
                    companyId = companiesDto.companyId,
                    CompanyName = companiesDto.CompanyName,
                };
                await _context.Companies.AddAsync(companiesdto);
                await _context.SaveChangesAsync();


                //Map DTO to Domain Model for the angular application
                var response = new CompaniesAngularDto
                {
                    Id = companiesdto.Id,
                    companyId = companiesdto.companyId,
                    CompanyName = companiesdto.CompanyName,
                };


                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
            //await companyRepository.AddCompanies(companies);
            //return Ok();
        }


        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCompanies(int id, CompaniesDto companiesDto)
        {

            try
            {
                //Map DTO to Domain Model
                var company = new Companies
                {
                    Id = id,
                    companyId = companiesDto.companyId,
                    CompanyName = companiesDto.CompanyName,
                };
                var data = await companyRepository.UpdateAsync(id,company);
                if(data == null)
                {
                    return NotFound();
                }

                //Map DTO to Domain Model for the angular application
                var response = new CompaniesAngularDto
                {
                    Id = company.Id,
                    companyId = company.companyId,
                    CompanyName = company.CompanyName
                };


                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("Remove")]
        public async Task<IActionResult> Remove(int id)
        {
            await companyRepository.DeleteCompanies(id);
            return Ok();
        }
    } 
}
