
namespace IrmandadeDoCodigo.Hub.Api.ViewModels.Post
{
    public class FindPaginatedPostsViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Owner { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
