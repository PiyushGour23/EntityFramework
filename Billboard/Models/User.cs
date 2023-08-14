using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Billboard.Models
{
    public class User
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public enum Priority
        {
            Low,Medium,High
        }
    }
}
