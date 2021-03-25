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
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext context;

        public ArticleRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Article Article, CancellationToken token)
        {
            await context.Articles.AddAsync(Article, token);
        }

        public async Task<bool> Exists(Guid Id, CancellationToken token)
        {
            return await context.Articles.CountAsync(a => a.Id == Id, token) > 0;
        }

        public async Task<IEnumerable<Article>> GetAllAsync(CancellationToken token)
        {
            return await context.Articles.ToListAsync(token);
        }

        public async Task<Article> GetByIdAsync(Guid Id, CancellationToken token)
        {
            return await context.Articles.FirstOrDefaultAsync(a => a.Id == Id, token);
        }

        public Guid NextIdentifier()
        {
            return Guid.NewGuid();
        }

        public async Task RemoveAsync(Article Article, CancellationToken token)
        {
            context.Articles.Remove(Article);
            await SaveAsync(token);
        }

        public async Task SaveAsync(CancellationToken token)
        {
            await context.SaveChangesAsync(token);
        }
    }
}
