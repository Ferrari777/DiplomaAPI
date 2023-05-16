using DiplomaAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.DTOs
{
    public class UserRoleDto
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
    }
}
