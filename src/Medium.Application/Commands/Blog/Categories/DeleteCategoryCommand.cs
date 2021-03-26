using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Entities;
using Medium.Domain.Blog.Exceptions;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Commands.Blog.Categories
{
    public static class DeleteCategoryCommand
    {
        public record DeleteCategoryRequest(Guid Id) : IRequest<bool>;
        public class Handler : IRequestHandler<DeleteCategoryRequest, bool>
        {
            private readonly ICategoryRepository repository;

            public Handler(ICategoryRepository repository)
            {
                this.repository = repository;
            }
            public async Task<bool> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
            {
                var category = await repository.GetByIdAsync(request.Id, cancellationToken);
                await repository.RemoveAsync(category, cancellationToken);
                return true;
            }
        }
    }
}
