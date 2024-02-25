using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IrmandadeDoCodigo.Hub.Api.Models
{
    [Table("Reactions")]
    [Index(nameof(Id))]
    public class Reaction
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public required Post Post { get; set; }
        [Required]
        public required User User { get; set; }
        public required ReactionType React { get; set; }
    }

    public enum ReactionType
    {
        ENGAGED = 0,
        LIKED = 0,
        DISLIKED = 2,
    }
}
