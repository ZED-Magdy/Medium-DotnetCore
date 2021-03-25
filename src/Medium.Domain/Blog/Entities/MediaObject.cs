using System;

namespace Medium.Domain.Blog.Entities
{
    public class MediaObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Path { get; set; }
        public string Alternative { get; set; }
        public Guid BlogId { get; set; }
    }
}
