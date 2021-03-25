using System;

namespace Medium.Domain.Blog.Exceptions
{
    public class TagNotFoundException : System.Exception
    {
        public TagNotFoundException(Guid Id) : base($"Tag of Id {Id} is not found !") { }
    }
}
