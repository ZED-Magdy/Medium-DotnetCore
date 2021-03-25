using System;

namespace Medium.Domain.Blog.Exceptions
{
    public class ArticleNotFoundException : System.Exception
    {
        public ArticleNotFoundException(Guid Id) : base($"Article of Id {Id} is not found !") { }
    }
}
