using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infraestructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Repositories
{
    public class PostRepository:IPostRepository
    {
        private readonly SocialMediaContext _context;

        public PostRepository(SocialMediaContext cotnext)
        {
            _context = cotnext;
        }

        public async Task<Post> GetPostById(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            return post;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts =await _context.Posts.ToListAsync();
            
            return posts;
        }
        public async Task InsertPost(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }
    }
}
