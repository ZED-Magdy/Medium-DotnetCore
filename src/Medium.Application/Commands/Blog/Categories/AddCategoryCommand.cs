using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Entities;
using Medium.Domain.Blog.Exceptions;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Commands.Blog.Categories
{
    public static class AddCategoryCommand
    {
        public record AddCategoryRequest(string Name) : IRequest<Guid>;
        public class Handler : IRequestHandler<AddCategoryRequest, Guid>
        {
            private readonly ICategoryRepository repository;

            public Handler(ICategoryRepository repository)
            {
                this.repository = repository;
            }
            public async Task<Guid> Handle(AddCategoryRequest request, CancellationToken cancellationToken)
            {
                if(await repository.CategoryNameExist(request.Name, cancellationToken))
                {
                    throw new CategoryNameAlreadyExistException(request.Name);
                }
                Guid Id = repository.NextIdentifier();
                await repository.AddAsync(new Category
                {
                    Id = Id,
                    Name = request.Name
                }, cancellationToken);
                return Id;
            }
        }
    }
}
