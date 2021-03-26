using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Entities;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Queries.Blog.Articles
{
    public static class GetArticleTags
    {
        public record Query(Guid ArticleId) : IRequest<IEnumerable<Tag>>;
        public class Handler : IRequestHandler<Query, IEnumerable<Tag>>
        {
            private readonly ITagRepository repository;

            public Handler(ITagRepository repository)
            {
                this.repository = repository;
            }

            public async Task<IEnumerable<Tag>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await repository.GetByArticleIdAsync(request.ArticleId, cancellationToken);
            }
        }
    }
}
