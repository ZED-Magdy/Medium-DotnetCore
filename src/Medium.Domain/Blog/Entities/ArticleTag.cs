using System;
using System.Collections.Generic;

namespace Medium.Domain.Blog.Entities
{
    public class ArticleTag
    {
        public Guid ArticleId { get; set; }
        public Guid TagId { get; set; }
        public Tag Tag { get; set; }
        public Article Article { get; set; }
    }
}
