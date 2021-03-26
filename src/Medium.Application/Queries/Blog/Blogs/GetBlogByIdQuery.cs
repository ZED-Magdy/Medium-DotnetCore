using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Queries.Blog.Blogs
{
    public static class GetBlogByIdQuery
    {
        public record Query(Guid Id) : IRequest<Domain.Blog.Entities.Blog>;
        public class Handler : IRequestHandler<Query, Domain.Blog.Entities.Blog>
        {
            private readonly IBlogRepository repository;

            public Handler(IBlogRepository repository)
            {
                this.repository = repository;
            }
            public async Task<Domain.Blog.Entities.Blog> Handle(Query request, CancellationToken cancellationToken)
            {
                return await repository.GetByIdAsync(request.Id, cancellationToken);
            }
        }
    }
}
