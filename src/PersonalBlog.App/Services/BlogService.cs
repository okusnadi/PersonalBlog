using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalBlog.App.Data;
using SAFe.Infrastructure.Services;

namespace PersonalBlog.App.Services
{
    /// <summary>
    /// Represents the service of blog operations.
    /// </summary>
    public class BlogService : CrudServiceProvider
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BlogService"/> class.
        /// </summary>
        /// <param name="services">instance of <see cref="IServiceProvider"/> .</param>
        public BlogService(IServiceProvider services) : base(services)
        {
        }

        /// <summary>
        /// Asynchrosouly gets the blog posts with the <see cref="PostListSpecification"/> class.
        /// </summary>
        /// <returns>A task that represents a get list operation. The result contains the list of <see cref="Post"/> class.</returns>
        public virtual Task<List<Post>> GetPostsAsync(string categoryName = default)
            => GetListAsync<Post>(new PostListSpecification(), post =>
            {
                post= post.Include(m => m.Category);

                if (!string.IsNullOrEmpty(categoryName))
                {
                    post = post.Where(m => m.Category.Name.Equals(categoryName));
                }
                return post;
            });

        /// <summary>
        /// Asynchrosouly gets the post categories with the <see cref="CategoryListSpecification"/> class.
        /// </summary>
        /// <returns>A task that represents a get list operation. The result contains the list of <see cref="PostCategory"/> class.</returns>
        public virtual Task<List<PostCategory>> GetPostCategoriesAsync()
            => GetListAsync<PostCategory>(new CategoryListSpecification());

        /// <summary>
        /// Asynchrosouly update the view count of post by specified id and offset. Nothing happend if post was not found.
        /// </summary>
        /// <param name="postId">The post id to update.</param>
        /// <param name="offset">The offset to increase.</param>
        /// <returns>A task represent an update operation. The task does not contains any results.</returns>
        public async Task UpdateViewCount(string postId, int offset=1)
        {
            var post = await FindAsync<Post>(postId);
            if (post != null)
            {
                post.SetViewCount(offset);
                await base.GetScopedContext().SaveChangesAsync(CancellationToken);
            }
        }
    }
}
