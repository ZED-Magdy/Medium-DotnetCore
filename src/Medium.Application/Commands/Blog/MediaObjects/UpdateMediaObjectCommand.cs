using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Exceptions;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Commands.Blog.MediaObjects
{
    public static class UpdateMediaObjectCommand
    {
        public record UpdateMediaObjectRequest(Guid Id, string Alternative, Guid BlogId) : IRequest<bool>;
        public class Handler : IRequestHandler<UpdateMediaObjectRequest, bool>
        {
            private readonly IMediaObjectRepository repository;

            public Handler(IMediaObjectRepository repository)
            {
                this.repository = repository;
            }
            public async Task<bool> Handle(UpdateMediaObjectRequest request, CancellationToken cancellationToken)
            {
                var media = await repository.GetByIdAsync(request.Id, cancellationToken);
                if(media.BlogId != request.BlogId)
                {
                    throw new MediaObjectNotFoundException(request.Id);
                }
                media.Alternative = request.Alternative;
                return true;
            }
        }
    }
}
