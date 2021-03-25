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
    public class TagRepository : ITagRepository
    {
        private readonly AppDbContext context;

        public TagRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Tag Tag, CancellationToken token)
        {
            await context.Tags.AddAsync(Tag, token);
        }

        public async Task<bool> Exists(Guid Id, CancellationToken token)
        {
            return await context.Tags.CountAsync(a => a.Id == Id, token) > 0;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync(CancellationToken token)
        {
            return await context.Tags.ToListAsync(token);
        }

        public async Task<Tag> GetByIdAsync(Guid Id, CancellationToken token)
        {
            return await context.Tags.FirstOrDefaultAsync(t => t.Id == Id, token);
        }

        public Guid NextIdentifier()
        {
            return Guid.NewGuid();
        }

        public async Task RemoveAsync(Tag Tag, CancellationToken token)
        {
            context.Tags.Remove(Tag);
            await SaveAsync(token);
        }

        public async Task SaveAsync(CancellationToken token)
        {
            await context.SaveChangesAsync(token);
        }
    }
}
