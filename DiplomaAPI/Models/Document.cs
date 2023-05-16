using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.Models
{
    public class Document
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(500)]
        [Unicode(true)]
        public string? DocumentName { get; set; }
        
        [Required]
        [MaxLength(100)]
        [Unicode(false)]
        public string? ContentType { get; set; }

        public long? FileSize { get; set; }
    }
}
