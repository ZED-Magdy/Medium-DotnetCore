using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Entities;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Queries.Blog.MediaObjects
{
    public static class GetMediaObjectsQuery
    {
        //TODO: add pagination/filters
        public record Query(Guid BlogId) : IRequest<IEnumerable<MediaObject>>;

        public class Handler : IRequestHandler<Query, IEnumerable<MediaObject>>
        {
            private readonly IMediaObjectRepository repository;

            public Handler(IMediaObjectRepository repository)
            {
                this.repository = repository;
            }
            public async Task<IEnumerable<MediaObject>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await repository.GetAllAsync(cancellationToken);
            }
        }
    }
}
