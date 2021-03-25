using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Entities;
using Medium.Domain.Blog.Exceptions;
using Medium.Domain.Blog.Repositories;

namespace Medium.Application.Commands.Blog.Articles
{
    public static class AddArticleCommand
    {
        public record AddArticleRequest
        (
            [Required]string Title,
            [Required]string Content,
            string Excerpt,
            [Required]Guid BlogId,
            Guid ThumbnailId,
            [Required]Guid CategoryId,
            [Required]IList<Guid> TagsId
        ) : IRequest<Guid>;
        public class Handler : IRequestHandler<AddArticleRequest, Guid>
        {
            private readonly IArticleRepository repository;
            private readonly ICategoryRepository categoryRepository;
            private readonly IBlogRepository blogRepository;
            private readonly ITagRepository tagRepository;
            private readonly IMediaObjectRepository mediaObjectRepository;

            public Handler
            (
                IArticleRepository repository,
                ICategoryRepository categoryRepository,
                IBlogRepository blogRepository,
                ITagRepository tagRepository,
                IMediaObjectRepository mediaObjectRepository
            )
            {
                this.repository = repository;
                this.categoryRepository = categoryRepository;
                this.blogRepository = blogRepository;
                this.tagRepository = tagRepository;
                this.mediaObjectRepository = mediaObjectRepository;
            }
            public async Task<Guid> Handle(AddArticleRequest request, CancellationToken cancellationToken)
            {
                //Check if category Exists
                if(! (await categoryRepository.Exists(request.CategoryId, cancellationToken)))
                {
                    throw new CategoryNotFoundException(request.CategoryId);
                }
                //Check if blog Exists
                if (!(await blogRepository.Exists(request.BlogId, cancellationToken)))
                {
                    throw new BlogNotFoundException(request.BlogId);
                }
                //Check if tags Exists
                List<Tag> tags = new List<Tag>();
                foreach(Guid tagId in request.TagsId)
                {
                    var tag = await tagRepository.GetByIdAsync(tagId, cancellationToken);
                    if (tag == null)
                    {
                        throw new CategoryNotFoundException(tagId);
                    }
                    tags.Add(tag);
                }
                //check if MediaObject exists
                if(request.ThumbnailId != null)
                {

                    if (!(await mediaObjectRepository.Exists(request.ThumbnailId, cancellationToken)))
                    {
                        throw new BlogNotFoundException(request.ThumbnailId);
                    }
                }
                Guid Id = repository.NextIdentifier();
                await repository.AddAsync(new Article
                {
                    Title = request.Title,
                    Content = request.Content,
                    Excerpt = request.Excerpt ?? request.Content.Substring(20),
                    BlogId = request.BlogId,
                    CategoryId = request.CategoryId,
                    ThumbnailId = request.ThumbnailId,
                    Tags = tags
                }, cancellationToken);
                return Id;
            }
        }
    }
}
