using System;
using System.Linq;
using System.Linq.Expressions;
using SAFe.Infrastructure.Services;

namespace PersonalBlog.App.Data
{
    public class ListSpecification : Specification<Post>
    {

        public override Expression<Func<Post, bool>> IsSatisfiedBy => entity => true;

        public override Expression<Func<IQueryable<Post>, IOrderedQueryable<Post>>> OrderedBy 
            => entity 
            => entity.OrderByDescending(m => m.IsTop)
                        .ThenByDescending(m => m.CreatedTime);
    }
}
