using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Medium.Domain.Blog.Entities;
using Medium.Domain.Blog.Repositories;
using Microsoft.AspNetCore.Http;

namespace Medium.Application.Commands.Blog.MediaObjects
{
    public static class AddMediaObjectCommand
    {
        public record AddMediaObjectRequest(Guid BlogId, IFormFile file, string baseUri) : IRequest<Guid>;
        public class Handler : IRequestHandler<AddMediaObjectRequest, Guid>
        {
            private readonly IMediaObjectRepository repository;

            public Handler(IMediaObjectRepository repository)
            {
                this.repository = repository;
            }
            public async Task<Guid> Handle(AddMediaObjectRequest request, CancellationToken cancellationToken)
            {
                var Name = await UploadFile(request.file);
                var Id = repository.NextIdentifier();
                var mediaObject = new MediaObject
                {
                    Id = Id,
                    Name = Name,
                    Alternative = null,
                    Path = $"Resources/Images/{Name}",
                    BlogId = request.BlogId,
                    Url = $"{request.baseUri}/Resources/Images/{Name}"
                };
                await repository.AddAsync(mediaObject, cancellationToken);
                return Id;
            }

            private static async Task<string> UploadFile(IFormFile file)
            {
                string imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "Resources/Images");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }
                string extension = extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                string fileName = DateTime.Now.Ticks + extension;
                string filePath = Path.Combine(imagesPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return fileName;
            }
        }
    }
}
