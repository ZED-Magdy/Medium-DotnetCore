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
    public class MediaObjectRepository : IMediaObjectRepository
    {
        private readonly AppDbContext context;

        public MediaObjectRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(MediaObject MediaObject, CancellationToken token)
        {
            await context.MediaObjects.AddAsync(MediaObject, token);
        }

        public async Task<bool> Exists(Guid Id, CancellationToken token)
        {
            return await context.MediaObjects.AsNoTracking().CountAsync(a => a.Id == Id, token) > 0;
        }

        public async Task<IEnumerable<MediaObject>> GetAllAsync(CancellationToken token)
        {
            return await context.MediaObjects.AsNoTracking().ToListAsync(token);
        }

        public async Task<MediaObject> GetByIdAsync(Guid Id, CancellationToken token)
        {
            return await context.MediaObjects.FirstOrDefaultAsync(m => m.Id == Id, token);
        }

        public Guid NextIdentifier()
        {
            return Guid.NewGuid();
        }

        public async Task RemoveAsync(MediaObject MediaObject, CancellationToken token)
        {
            context.MediaObjects.Remove(MediaObject);
            await SaveAsync(token);
        }

        public async Task SaveAsync(CancellationToken token)
        {
            await context.SaveChangesAsync(token);
        }
    }
}
