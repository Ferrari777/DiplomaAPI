using System.ComponentModel.DataAnnotations;

namespace DiplomaAPI.Models
{
    public class DocFile
    {
        
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Author { get; set; }

        // Property of the chemical substance name
        // that can be described in the documentation file
        [Required]
        public string? ChemSubstanceName { get; set; }

        public DateTime Created { get; set; }
    }
}
