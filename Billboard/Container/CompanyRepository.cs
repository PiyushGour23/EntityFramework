using AutoMapper;
using Billboard.Data;
using Billboard.Models;
using Billboard.Service;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
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
            //var records = await this.userDbContext.Companies.Select(x => new Companies()  -- For learning purpose
            //{
            //    Id = x.Id,
            //    companyId = x.companyId,
            //    CompanyName = x.CompanyName,
            //}).ToListAsync();
            //return records;
        }

        public async Task <List<Companies>> GetById(int id)
        {
            //userDbContext.Companies.FindAsync(id);
            //return userDbContext.Companies.ToList();
            try
            {
                var records = await this.userDbContext.Companies.Where(x => x.Id == id).Select(x => new Companies()
                {
                    Id = x.Id,
                    companyId = x.companyId,
                    CompanyName = x.CompanyName
                }).ToListAsync();
                return records;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Companies>> GetByCompanyId(int companyId)
        {
            try
            {
                var records = await this.userDbContext.Companies.Where(x => x.companyId == companyId).Select(x => new Companies()
                {
                    Id = x.Id,
                    companyId = x.companyId,
                    CompanyName = x.CompanyName
                }).ToListAsync();
                return records;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Companies> AddCompanies(Companies companies)
        {
            await userDbContext.Companies.AddAsync(companies);
            await userDbContext.SaveChangesAsync();
            return companies;
        }


        public async Task<Companies> UpdateAsync(int id, Companies companies)
        {
            var existingcompany = await userDbContext.Companies.FirstOrDefaultAsync(x => x.Id == companies.Id);
            if (existingcompany != null)
            {
                userDbContext.Entry(existingcompany).CurrentValues.SetValues(companies);
                await userDbContext.SaveChangesAsync();
                return companies;
            }
            return null;
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
