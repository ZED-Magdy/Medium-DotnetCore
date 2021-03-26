using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Medium.Domain.Blog.Entities;
using Medium.Domain.Common;

namespace Medium.Domain.Blog.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool> CategoryNameExist(string Name, CancellationToken token);
        Task<bool> CategoryNameExist(string Name, Guid Id, CancellationToken token);
    }
}
