using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SAFe;
using SAFe.Infrastructure.Services;

namespace PersonalBlog.App.Data
{
    public class BlogService
    {
        public BlogService(CrudServiceProvider crudServiceProvider)
        {
            CrudServiceProvider = crudServiceProvider;
        }

        public CrudServiceProvider CrudServiceProvider { get; }

        public virtual Task<List<Post>> GetPostsAsync()
            => CrudServiceProvider.GetListAsync<Post>(new ListSpecification());
    }
}
