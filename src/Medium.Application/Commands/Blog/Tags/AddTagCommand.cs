using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Entities;
using Medium.Domain.Blog.Exceptions;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Commands.Blog.Tags
{
    public class AddTagCommand
    {
        public record AddTagRequest(string Name) : IRequest<Guid>;
        public class Handler : IRequestHandler<AddTagRequest, Guid>
        {
            private readonly ITagRepository repository;

            public Handler(ITagRepository repository)
            {
                this.repository = repository;
            }
            public async Task<Guid> Handle(AddTagRequest request, CancellationToken cancellationToken)
            {
                if(await repository.NameExistAsync(request.Name, cancellationToken)){
                    throw new TagNameAlreadyExistException(request.Name);
                }
                Guid Id = repository.NextIdentifier();
                await repository.AddAsync(new Tag{
                    Id = Id,
                    Name = request.Name
                }, cancellationToken);
                return Id;
            }
        }
    }
}
