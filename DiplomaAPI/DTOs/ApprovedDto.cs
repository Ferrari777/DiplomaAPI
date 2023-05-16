using DiplomaAPI.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.DTOs
{
    public class ApprovedDto
    {
        public int Id { get; set; }

        [Required]
        public string? ApprovedBy { get; set; }

        [Required]
        public int? ApprovedOrder { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime ApprovedSince { get; set; }

        [Required]
        public int? ApprovedNumber { get; set; }

        public DateTime CreatedDatetime { get; set; }

        public DateTime UpdatedDatetime { get; set; }

        public int DocFileId { get; set; }
    }
}
