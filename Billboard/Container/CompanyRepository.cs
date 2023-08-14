using Billboard.Data;
using Billboard.Models;
using Billboard.Service;
using Microsoft.EntityFrameworkCore;

namespace Billboard.Container
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly UserDbContext userDbContext;
        public CompanyRepository(UserDbContext userDbContext) 
        { 
            this.userDbContext = userDbContext;
        }
        public List<Companies> GetCompanies()
        {
            return  this.userDbContext.Companies.ToList();
        }

        //public async Task<List<Companies>> AddCompanies(Companies companies)
        //{
        //    return await this.userDbContext.Companies.Add(companies);
        //}
    }
}
