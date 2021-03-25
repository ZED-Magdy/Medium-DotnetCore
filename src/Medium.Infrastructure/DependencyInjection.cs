using Medium.Infrastructure.Persistence.EFCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Medium.Domain.Blog.Repositories;
using Medium.Infrastructure.Persistence.EFCore.Blog.Repositories;

namespace Medium.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, string ConnectionString)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseNpgsql(ConnectionString);
            });
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IMediaObjectRepository, MediaObjectRepository>();
        }
    }
}
