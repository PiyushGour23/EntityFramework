using System.ComponentModel.DataAnnotations;

namespace Billboard.Models
{
    public class Equipment
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime ManufactureDate { get; set; }
        public DateTime ManufactureTime { get; set; }

    }
}
