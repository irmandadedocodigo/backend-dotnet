using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IrmandadeDoCodigo.Hub.Api.Models
{
    [Table("Post")]
    [Index(nameof(Id))]
    public class Post
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public required string Content { get; set; }
        [Required]
        public required User Owner { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
