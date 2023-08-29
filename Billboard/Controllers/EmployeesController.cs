using Billboard.Data;
using Billboard.Models;
using Billboard.Models.DTO;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office.CustomUI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Billboard.Controllers
{
    //[Authorize]
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
        public async Task<IActionResult> AddUser(EmployeesDto employeesDto)
        {
            try
            {
                //Map DTO to Domain Model
                var empdto = new Employees
                {
                    Title = employeesDto.Title,
                    FirstName = employeesDto.FirstName,
                    LastName = employeesDto.LastName,
                    Gender = employeesDto.Gender,
                    Email = employeesDto.Email,
                    CompanyId = employeesDto.CompanyId,
                };
                await _context.Employees.AddAsync(empdto);
                await _context.SaveChangesAsync();

                //Map DTO to Domain Model for angular application
                var response = new EmployeeAngularDto
                {
                    Id = empdto.Id,
                    Title = empdto.Title,
                    FirstName = empdto.FirstName,
                    LastName = empdto.LastName,
                    Gender = empdto.Gender,
                    Email = empdto.Email,
                    CompanyId = empdto.CompanyId,
                };

                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet("ExportToExcel")]
        public async Task<IActionResult> ExportToExcel()
        {
            try
            {
                var empdata = GetEmpData();
                var custdata = GetCustData();
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.AddWorksheet(empdata, "Employee table");
                    wb.AddWorksheet(custdata);
                    using(MemoryStream  ms = new MemoryStream())
                    {
                        wb.SaveAs(ms);
                        return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SampleSheet.xlsx");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        [NonAction]
        private DataTable GetEmpData()
        {
            DataTable dt = new DataTable();
            dt.TableName = "EmpTable";
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("title", typeof(string));
            dt.Columns.Add("firstname", typeof(string));
            dt.Columns.Add("lastname", typeof(string));
            dt.Columns.Add("gender", typeof(string));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("companyid", typeof(int));

            var list = _context.Employees.ToList();
            if(list.Count > 0)
            {
                list.ForEach(Item =>
                {
                    dt.Rows.Add(Item.Id, Item.Title, Item.FirstName, Item.LastName, Item.Gender, Item.Email, Item.CompanyId);
                });
            }
            return dt;
        }


        [NonAction]
        private DataTable GetCustData()
        {
            DataTable dt = new DataTable();
            dt.TableName = "CustTable";
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("compamyid", typeof(string));
            dt.Columns.Add("companyname", typeof(string));

            var list = _context.Companies.ToList();
            if (list.Count > 0)
            {
                list.ForEach(Item =>
                {
                    dt.Rows.Add(Item.Id, Item.companyId, Item.CompanyName);
                });
            }
            return dt;
        }

    }
}
