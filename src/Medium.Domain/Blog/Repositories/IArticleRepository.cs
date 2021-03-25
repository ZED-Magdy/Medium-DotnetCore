using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Medium.Domain.Blog.Entities;
using Medium.Domain.Common;

namespace Medium.Domain.Blog.Repositories
{
    public interface IArticleRepository : IRepository<Article>
    {
    }
}
