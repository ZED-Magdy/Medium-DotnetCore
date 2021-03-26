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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext context;

        public CategoryRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Category Category, CancellationToken token)
        {
            await context.Categories.AddAsync(Category, token);
        }

        public async Task<bool> CategoryNameExist(string Name, CancellationToken token)
        {
            return await context.Categories.CountAsync(c => c.Name == Name, token) > 0;
        }

        public async Task<bool> CategoryNameExist(string Name, Guid Id, CancellationToken token)
        {
            return await context.Categories.CountAsync(c => c.Id != Id && c.Name == Name, token) > 0;
        }
        public async Task<bool> Exists(Guid Id, CancellationToken token)
        {
            return await context.Categories.CountAsync(a => a.Id == Id, token) > 0;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken token)
        {
            return await context.Categories.ToListAsync(token);
        }

        public async Task<Category> GetByIdAsync(Guid Id, CancellationToken token)
        {
            return await context.Categories.FirstOrDefaultAsync(c => c.Id == Id, token);
        }

        public Guid NextIdentifier()
        {
            return Guid.NewGuid();
        }

        public async Task RemoveAsync(Category Category, CancellationToken token)
        {
            context.Categories.Remove(Category);
            await SaveAsync(token);
        }

        public async Task SaveAsync(CancellationToken token)
        {
            await context.SaveChangesAsync(token);
        }
    }
}
