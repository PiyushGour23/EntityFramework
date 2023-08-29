using System.ComponentModel.DataAnnotations;

namespace Billboard.Models
{
    public class Companies
    {
        public int Id { get; set; }
        public int companyId { get; set; }
        public string CompanyName { get; set; } 
    }
}
