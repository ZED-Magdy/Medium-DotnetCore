using System;
using System.Collections.Generic;
namespace Medium.Domain.Blog.Entities
{
    public class Blog
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public IList<Article> Articles { get; set; } = new List<Article>();
    }
}
