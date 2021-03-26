using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Entities;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Queries.Blog.Categories
{
    public static class GetCategoryByIdQuery
    {
        public record Query(Guid Id) : IRequest<Category>;
        public class Handler : IRequestHandler<Query, Category>
        {
            private readonly ICategoryRepository repository;

            public Handler(ICategoryRepository repository)
            {
                this.repository = repository;
            }
            public async Task<Category> Handle(Query request, CancellationToken cancellationToken)
            {
                return await repository.GetByIdAsync(request.Id, cancellationToken);
            }
        }
    }
}
