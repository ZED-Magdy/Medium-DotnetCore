using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Exceptions;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Commands.Blog.Articles
{
    public static class UpdateArticleCommand
    {
        public record UpdateArticleRequest
        (
            [Required] Guid Id,
            string Title,
            string Content,
            string Excerpt,
            Guid ThumbnailId,
            Guid CategoryId
        ) : IRequest<bool>;

        public class Handler : IRequestHandler<UpdateArticleRequest, bool>
        {
            private readonly IArticleRepository repository;
            private readonly ICategoryRepository categoryRepository;
            private readonly IMediaObjectRepository mediaObjectRepository;

            public Handler
            (
                IArticleRepository repository,
                ICategoryRepository categoryRepository,
                IMediaObjectRepository mediaObjectRepository
            )
            {
                this.repository = repository;
                this.categoryRepository = categoryRepository;
                this.mediaObjectRepository = mediaObjectRepository;
            }
            public async Task<bool> Handle(UpdateArticleRequest request, CancellationToken cancellationToken)
            {
                var article = await repository.GetByIdAsync(request.Id, cancellationToken);
                if(article == null)
                {
                    throw new ArticleNotFoundException(request.Id);
                }
                if(request.Title != null){
                    article.Title = request.Title;
                }
                if (request.Content != null)
                {
                    article.Content = request.Content;
                }
                if (request.Excerpt != null)
                {
                    article.Excerpt = request.Excerpt;
                }
                if (request.CategoryId != null)
                {
                    if(!(await categoryRepository.Exists(request.CategoryId, cancellationToken))){
                        throw new CategoryNotFoundException(request.CategoryId);
                    }
                    article.CategoryId = request.CategoryId;
                }
                if (request.ThumbnailId != null)
                {
                    if (!(await mediaObjectRepository.Exists(request.ThumbnailId, cancellationToken)))
                    {
                        throw new MediaObjectNotFoundException(request.ThumbnailId);
                    }
                    article.ThumbnailId = request.ThumbnailId;
                }
                await repository.SaveAsync(cancellationToken);
                
                return true;
            }
        }
    }
}
