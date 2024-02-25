using Microsoft.AspNetCore.Mvc;

namespace IrmandadeDoCodigo.Hub.Api.Controllers
{
    [ApiController]
    public class PostController : ControllerBase
    {
        [HttpGet("v1/posts")]
        public async Task<IActionResult> GetLastPosts(
                    [FromQuery] int page,
                    [FromQuery] int pageSize)
        {
            return Ok();
        }
    }
}
