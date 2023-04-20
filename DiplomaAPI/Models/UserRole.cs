using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.Models
{
    public class UserRole
    {
        public int Id { get; set; }

        [Required]
        public string? Type { get; set; }

        [Required]
        public string? Name { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
