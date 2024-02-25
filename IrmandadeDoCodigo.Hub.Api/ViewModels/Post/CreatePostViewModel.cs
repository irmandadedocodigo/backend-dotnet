using System.ComponentModel.DataAnnotations;

namespace IrmandadeDoCodigo.Hub.Api.ViewModels.Post
{
    public class CreatePostViewModel
    {
        [Required]
        public required string Content { get; set; }
    }
}
