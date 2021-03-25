using System;
using System.Collections.Generic;
namespace Medium.Domain.Blog.Entities
{
    public class Article
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Excerpt { get; set; }
        public Guid BlogId { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid ThumbnailId { get; set; }
        public MediaObject Thumbnail { get; set; }
        public IList<Tag> Tags { get; set; } = new List<Tag>();
    }
}
