using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Entities;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Queries.Blog.Articles
{
    public static class GetArticleByIdQuery
    {
        public record Query(Guid Id): IRequest<Article>;

        public class Handler : IRequestHandler<Query, Article>
        {
            private readonly IArticleRepository repository;

            public Handler(IArticleRepository repository){
                this.repository = repository;
            }
            public async Task<Article> Handle(Query request, CancellationToken cancellationToken)
            {
                return await repository.GetByIdAsync(request.Id, cancellationToken);
            }
        }
    }
}
