using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace IrmandadeDoCodigo.Hub.Api.Models
{
    [Table("Roles")]
    [Index(nameof(Id))]
    public class Role
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public List<User>? Users { get; set; }
    }
}
