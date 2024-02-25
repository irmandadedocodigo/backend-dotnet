using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IrmandadeDoCodigo.Hub.Api.Models
{
    [Table("Jobs")]
    [Index(nameof(Id))]
    public class Job
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public required string Title { get; set; }
        [Required]
        public required string Description { get; set; }
        public string? Link { get; set; }
        [Required]
        public required User Publisher { get; set; }
    }
}
