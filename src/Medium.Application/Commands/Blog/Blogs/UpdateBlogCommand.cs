using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Exceptions;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Commands.Blog.Blogs
{
    public static class UpdateBlogCommand
    {
        public record UpdateBlogRequest(Guid BlogId, [Required]string Name, Guid OwnerId) : IRequest<bool>;
        public class Handler : IRequestHandler<UpdateBlogRequest, bool>
        {
            private readonly IBlogRepository repository;

            public Handler(IBlogRepository repository)
            {
                this.repository = repository;
            }
            public async Task<bool> Handle(UpdateBlogRequest request, CancellationToken cancellationToken)
            {
                var blog = await repository.GetByIdAsync(request.BlogId, cancellationToken);
                if(blog == null || blog.OwnerId != request.OwnerId)
                {
                    throw new BlogNotFoundException(request.BlogId);
                }
                blog.Name = request.Name;
                await repository.SaveAsync(cancellationToken);
                return true;
            }
        }
    }
}
