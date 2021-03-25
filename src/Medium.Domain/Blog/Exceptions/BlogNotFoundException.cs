using System;

namespace Medium.Domain.Blog.Exceptions
{
    public class BlogNotFoundException : System.Exception
    {
        public BlogNotFoundException(Guid Id) : base($"Blog of Id {Id} is not found !") { }
    }
}
