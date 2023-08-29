using System.ComponentModel.DataAnnotations;

namespace Billboard.Models.DTO
{
    public class EmployeesDto
    {
        public string Title { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int CompanyId { get; set; }
    }
}
