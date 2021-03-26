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
    public class UpdateTagCommand
    {
        public record UpdateTagRequest(Guid Id, string Name) : IRequest<bool>;
        public class Handler : IRequestHandler<UpdateTagRequest, bool>
        {
            private readonly ITagRepository repository;

            public Handler(ITagRepository repository)
            {
                this.repository = repository;
            }
            public async Task<bool> Handle(UpdateTagRequest request, CancellationToken cancellationToken)
            {
                if (await repository.NameExistAsync(request.Name, request.Id, cancellationToken))
                {
                    throw new TagNameAlreadyExistException(request.Name);
                }
                var tag = await repository.GetByIdAsync(request.Id, cancellationToken);
                if (tag == null)
                {
                    throw new TagNotFoundException(request.Id);
                }
                tag.Name = request.Name;
                await repository.SaveAsync(cancellationToken);
                return true;
            }
        }
    }
}
