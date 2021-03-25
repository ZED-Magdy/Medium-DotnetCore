using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Medium.Domain.Blog.Entities;
using Medium.Domain.Blog.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Medium.Infrastructure.Persistence.EFCore.Blog.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext context;

        public BlogRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Domain.Blog.Entities.Blog Blog, CancellationToken token)
        {
            await context.Blogs.AddAsync(Blog, token);
        }

        public async Task<bool> Exists(Guid Id, CancellationToken token)
        {
            return await context.Blogs.CountAsync(a => a.Id == Id, token) > 0;
        }

        public async Task<IEnumerable<Domain.Blog.Entities.Blog>> GetAllAsync(CancellationToken token)
        {
            return await context.Blogs.ToListAsync(token);
        }

        public async Task<Domain.Blog.Entities.Blog> GetByIdAsync(Guid Id, CancellationToken token)
        {
            return await context.Blogs.FirstOrDefaultAsync(b => b.Id == Id, token);
        }

        public Guid NextIdentifier()
        {
            return Guid.NewGuid();
        }

        public async Task RemoveAsync(Domain.Blog.Entities.Blog Blog, CancellationToken token)
        {
            context.Blogs.Remove(Blog);
            await SaveAsync(token);
        }

        public async Task SaveAsync(CancellationToken token)
        {
            await context.SaveChangesAsync(token);
        }
    }
}
