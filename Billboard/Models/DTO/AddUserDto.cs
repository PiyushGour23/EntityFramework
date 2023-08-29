using System.ComponentModel.DataAnnotations;

namespace Billboard.Models.DTO
{
    public class AddUserDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public enum Priority
        {
            Low, Medium, High
        }
    }
}
