using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Entities;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Queries.Blog.Categories
{
    public static class GetCategoriesQuery
    {
        //TODO: add pagination/filters
        public record Query() : IRequest<IEnumerable<Category>>;
        public class Handler : IRequestHandler<Query, IEnumerable<Category>>
        {
            private readonly ICategoryRepository repository;

            public Handler(ICategoryRepository repository)
            {
                this.repository = repository;
            }
            public async Task<IEnumerable<Category>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await repository.GetAllAsync(cancellationToken);
            }
        }
    }
}
