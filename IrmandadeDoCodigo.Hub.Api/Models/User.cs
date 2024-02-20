using System.Text.Json.Serialization;

namespace IrmandadeDoCodigo.Hub.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public IList<Role> Roles { get; set; }
    }
}
