using System.ComponentModel.DataAnnotations;

namespace Billboard.Models.DTO
{
    public class CompaniesAngularDto
    {
        public int Id { get; set; }
        public int companyId { get; set; }
        public string CompanyName { get; set; }
    }
}
