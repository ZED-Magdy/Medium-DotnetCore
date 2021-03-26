using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Queries.Blog.Blogs
{
    public static class GetBlogsQuery
    {
        //TODO: Add Pagination/Filter
        public record Query() : IRequest<IEnumerable<Domain.Blog.Entities.Blog>>;
        public class Handler : IRequestHandler<Query, IEnumerable<Domain.Blog.Entities.Blog>>
        {
            private readonly IBlogRepository repository;

            public Handler(IBlogRepository repository)
            {
                this.repository = repository;
            }
            public async Task<IEnumerable<Domain.Blog.Entities.Blog>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await repository.GetAllAsync(cancellationToken);
            }
        }
    }
}
