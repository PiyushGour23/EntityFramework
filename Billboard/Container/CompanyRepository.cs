using Billboard.Data;
using Billboard.Models;
using Billboard.Service;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Mvc;
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
        public async Task <IEnumerable<Companies>> GetCompanies()
        {
            return await this.userDbContext.Companies.ToListAsync();
        }

        public List<Companies> GetById(int id)
        {
            userDbContext.Companies.FindAsync(id);
            return userDbContext.Companies.ToList();
            
        }
        public async Task<Companies> AddCompanies(Companies companies)
        {
            await userDbContext.Companies.AddAsync(companies);
            await userDbContext.SaveChangesAsync();
            return companies;
        }

        public async Task<string> DeleteCompanies(int id)
        {
            var userid = await userDbContext.Companies.FindAsync(id);
            userDbContext.Companies.Remove(userid);
            await userDbContext.SaveChangesAsync();
            return string.Empty;
        }
    }
}
