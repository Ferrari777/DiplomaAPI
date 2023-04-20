using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.Models
{
    public class DocFile
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Path { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public Agreed? Agreed { get; set; }

        public Approved? Approved { get; set; }
    }
}
