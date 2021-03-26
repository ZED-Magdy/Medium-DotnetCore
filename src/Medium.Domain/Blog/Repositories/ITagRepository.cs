using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Medium.Domain.Blog.Entities;
using Medium.Domain.Common;

namespace Medium.Domain.Blog.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task<IEnumerable<Tag>> GetByArticleIdAsync(Guid ArticleId, CancellationToken token);
    }
}
