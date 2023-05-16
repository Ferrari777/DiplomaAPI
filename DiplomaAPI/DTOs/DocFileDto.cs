using DiplomaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.DTOs
{
    public class DocFileDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        [Unicode(true)]
        public string? Name { get; set; }

        //[Required]
        //public string? Path { get; set; }

        [Required]
        [MaxLength(100)]
        [Unicode(false)]
        public string? ContentType { get; set; }

        public long? FileSize { get; set; }

        public int UserId { get; set; }

        public Agreed? Agreed { get; set; }

        public Approved? Approved { get; set; }
    }
}
