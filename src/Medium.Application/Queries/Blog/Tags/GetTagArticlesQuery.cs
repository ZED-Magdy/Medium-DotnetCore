using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Entities;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Queries.Blog.Tags
{
    public static class GetTagArticlesQuery
    {
        //TODO: add pagination/filters
        public record Query(Guid TagId) : IRequest<IEnumerable<Article>>;
        public class Handler : IRequestHandler<Query, IEnumerable<Article>>
        {
            private readonly IArticleRepository repository;

            public Handler(IArticleRepository repository)
            {
                this.repository = repository;
            }
            public async Task<IEnumerable<Article>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await repository.GetByTagAsync(request.TagId, cancellationToken);
            }
        }
    }
}
