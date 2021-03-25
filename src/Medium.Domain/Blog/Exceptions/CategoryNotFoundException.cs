using System;

namespace Medium.Domain.Blog.Exceptions
{
    public class CategoryNotFoundException : System.Exception
    {
        public CategoryNotFoundException(Guid Id) : base($"Category of Id {Id} is not found !") { }
    }
}
