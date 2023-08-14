using Billboard.Models;

namespace Billboard.Service
{
    public interface ICompanyRepository
    {
        List<Companies> GetCompanies();
        //Task<List<Companies>> AddCompanies(Companies companies);
    }
}
