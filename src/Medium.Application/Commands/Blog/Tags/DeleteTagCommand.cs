using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Exceptions;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Commands.Blog.Tags
{
    public static class DeleteTagCommand
    {
        public record DeleteTagRequest(Guid Id) : IRequest<bool>;
        public class Handler : IRequestHandler<DeleteTagRequest, bool>
        {
            private readonly ITagRepository repository;

            public Handler(ITagRepository repository)
            {
                this.repository = repository;
            }
            public async Task<bool> Handle(DeleteTagRequest request, CancellationToken cancellationToken)
            {
                var tag = await repository.GetByIdAsync(request.Id, cancellationToken);
                if(tag == null)
                {
                    throw new TagNotFoundException(request.Id);
                }
                await repository.RemoveAsync(tag, cancellationToken);
                return true;
            }
        }
    }
}
