using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PersonalBlog.App.Data;
using SAFe.Infrastructure.Services;

namespace PersonalBlog.App.Services
{
    public class CategoryListSpecification : Specification<PostCategory>
    {
        public override Expression<Func<PostCategory, bool>> IsSatisfiedBy
            => entity => true;

        public override Expression<Func<IQueryable<PostCategory>, IOrderedQueryable<PostCategory>>> OrderedBy
            => entity => entity.OrderBy(m => m.SortNo).ThenByDescending(m => m.Id);
    }
}
