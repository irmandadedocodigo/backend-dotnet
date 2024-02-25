using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace IrmandadeDoCodigo.Hub.Api.Models
{
    [Table("Users")]
    [Index(nameof(Id))]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Email { get; set; }
        [JsonIgnore]
        public required string PasswordHash { get; set; }
        public List<Role> Roles { get; set; } = [];
    }
}
