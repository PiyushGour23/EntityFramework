using Billboard.Models;
using Microsoft.AspNetCore.Mvc;

namespace Billboard.Service
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Companies>> GetCompanies();
        Task<List<Companies>> GetById(int id);
        Task<Companies> AddCompanies(Companies companies);
        Task<Companies> UpdateAsync(int id, Companies companies);
        Task<string> DeleteCompanies(int id);
        Task<List<Companies>> GetByCompanyId(int companyId);
    }
}
