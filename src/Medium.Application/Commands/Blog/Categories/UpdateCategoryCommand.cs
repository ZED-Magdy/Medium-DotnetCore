using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Entities;
using Medium.Domain.Blog.Exceptions;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Commands.Blog.Categories
{
    public static class UpdateCategoryCommand
    {
        public record UpdateCategoryRequest(Guid Id,string Name) : IRequest<bool>;
        public class Handler : IRequestHandler<UpdateCategoryRequest, bool>
        {
            private readonly ICategoryRepository repository;

            public Handler(ICategoryRepository repository)
            {
                this.repository = repository;
            }
            public async Task<bool> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
            {
                if (await repository.CategoryNameExist(request.Name, request.Id, cancellationToken))
                {
                    throw new CategoryNameAlreadyExistException(request.Name);
                }
                var category = await repository.GetByIdAsync(request.Id, cancellationToken);
                category.Name = request.Name;
                await repository.SaveAsync(cancellationToken);
                return true;
            }
        }
    }
}
