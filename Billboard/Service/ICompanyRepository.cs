using Billboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace Billboard.Service
{
    public interface ICompanyRepository
    {
        List<Companies> GetCompanies();
        List<Companies> GetById(int id);
        Task<Companies> AddCompanies(Companies companies);
        Task<string> DeleteCompanies(int id);
    }
}
