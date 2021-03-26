using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Entities;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Queries.Blog.MediaObjects
{
    public static class GetMediaObjectByIdQuery
    {
        public record Query(Guid Id) : IRequest<MediaObject>;

        public class Handler : IRequestHandler<Query, MediaObject>
        {
            private readonly IMediaObjectRepository repository;

            public Handler(IMediaObjectRepository repository)
            {
                this.repository = repository;
            }
            public async Task<MediaObject> Handle(Query request, CancellationToken cancellationToken)
            {
                return await repository.GetByIdAsync(request.Id, cancellationToken);
            }
        }
    }
}
