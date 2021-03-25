using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Exceptions;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Commands.Blog.Articles
{
    public static class DeleteArticleCommand
    {
        public record DeleteArticleRequest(Guid Id) : IRequest<bool>;
        public class Handler : IRequestHandler<DeleteArticleRequest, bool>
        {
            private readonly IArticleRepository repository;

            public Handler(IArticleRepository repository)
            {
                this.repository = repository;
            }
            public async Task<bool> Handle(DeleteArticleRequest request, CancellationToken cancellationToken)
            {
                var article = await repository.GetByIdAsync(request.Id, cancellationToken);
                if(article == null) {
                    throw new ArticleNotFoundException(request.Id);
                }
                await repository.RemoveAsync(article, cancellationToken);
                return true;
            }
        }
    }
}
