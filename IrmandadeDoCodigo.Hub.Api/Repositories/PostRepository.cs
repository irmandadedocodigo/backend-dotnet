using IrmandadeDoCodigo.Hub.Api.Data;
using IrmandadeDoCodigo.Hub.Api.Models;
using IrmandadeDoCodigo.Hub.Api.ViewModels.Post;
using Microsoft.EntityFrameworkCore;

namespace IrmandadeDoCodigo.Hub.Api.Repositories
{
    public class PostRepository(AppDataContext context)
    {
        public async Task<Post> Create(dynamic model, User user)
        {
            var post = new Post()
            {
                Id = Guid.NewGuid(),
                Content = model.Content,
                Owner = user,
                CreatedAt = DateTime.Now,
            };
            context.Posts.Add(post);
            await context.SaveChangesAsync();
            return post;
        }

        public async Task<Post?> FindById(Guid id)
        {
            var post = await context
                    .Posts
                    .AsNoTracking()
                    .Include(x => x.Owner)
                    .FirstOrDefaultAsync(x => x.Id == id);
            return post;
        }

        public async Task<List<FindPaginatedPostsViewModel>> FindPaginated(int page, int pageSize)
        {
            var posts = await context
                    .Posts
                    .AsNoTracking()
                    .Include(x => x.Owner)
                    .Select(x => new FindPaginatedPostsViewModel()
                    {
                        Id = x.Id,
                        Content = x.Content,
                        Owner = x.Owner.Name,
                        CreatedAt = x.CreatedAt,
                        UpdatedAt = x.UpdatedAt,
                    })
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync();
            return posts;
        }
    }
}
