using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Commands.Blog.Blogs
{
    public static class AddBlogCommand
    {
        public record AddBlogRequest([Required]string Name, Guid OwnerId) : IRequest<Guid>;

        public class Handler : IRequestHandler<AddBlogRequest, Guid>
        {
            private readonly IBlogRepository repository;

            public Handler(IBlogRepository repository)
            {
                this.repository = repository;
            }
            public async Task<Guid> Handle(AddBlogRequest request, CancellationToken cancellationToken)
            {
                //TODO: Check if name already Exist
                //TODO: Check if the user can create new blog
                Guid Id = repository.NextIdentifier();
                await repository.AddAsync(new Domain.Blog.Entities.Blog
                {
                    Id = Id,
                    Name = request.Name,
                    OwnerId = request.OwnerId
                }, cancellationToken);
                return Id;
            }
        }
    }
}
