using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiplomaAPI.Models
{
    public class Agreed
    {
        public int Id { get; set; }

        [Required]
        public string? AgreedBy { get; set; }

        [Required]
        public int? AgreedNumber { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime? AgreedSinceDate { get; set; }

        public DateTime CreatedDatetime { get; set; }

        public DateTime UpdatedDatetime { get; set; }

        public int DocFileId { get; set; }
        public DocFile? DocFile { get; set; }
    }
}
