using System;

namespace Medium.Domain.Blog.Exceptions
{
    public class MediaObjectNotFoundException : System.Exception
    {
        public MediaObjectNotFoundException(Guid Id) : base($"MediaObject of Id {Id} is not found !") { }
    }
}
