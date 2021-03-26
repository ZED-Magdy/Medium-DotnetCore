using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Exceptions;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Commands.Blog.Blogs
{
    public static class DeleteBlogCommand
    {
        public record DeleteBlogRequest(Guid BlogId, Guid OwnerId) : IRequest<bool>;
        public class Handler : IRequestHandler<DeleteBlogRequest, bool>
        {
            private readonly IBlogRepository repository;

            public Handler(IBlogRepository repository)
            {
                this.repository = repository;
            }
            public async Task<bool> Handle(DeleteBlogRequest request, CancellationToken cancellationToken)
            {
                var blog = await repository.GetByIdAsync(request.BlogId, cancellationToken);
                if(blog.OwnerId != request.OwnerId){
                    throw new BlogNotFoundException(request.BlogId);
                }
                await repository.RemoveAsync(blog, cancellationToken);
                return true;
            }
        }
    }
}
