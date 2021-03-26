using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Exceptions;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Commands.Blog.MediaObjects
{
    public static class DeleteMediaObjectCommand
    {
        public record DeleteMediaObjectRequest(Guid Id, Guid BlogId) : IRequest<bool>;
        public class Handler : IRequestHandler<DeleteMediaObjectRequest, bool>
        {
            private readonly IMediaObjectRepository repository;

            public Handler(IMediaObjectRepository repository)
            {
                this.repository = repository;
            }
            public async Task<bool> Handle(DeleteMediaObjectRequest request, CancellationToken cancellationToken)
            {
                var media = await repository.GetByIdAsync(request.Id, cancellationToken);
                if (media.BlogId != request.BlogId)
                {
                    throw new MediaObjectNotFoundException(request.Id);
                }
                await repository.RemoveAsync(media, cancellationToken);
                return true;
            }
        }
    }
}
