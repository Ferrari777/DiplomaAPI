using DiplomaAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Email { get; set; }

        public DateTime CreatedDatetime { get; set; }

        public DateTime UpdatedDatetime { get; set; }

        public int UserRoleId { get; set; }
    }
}
