using System;

namespace Medium.Domain.Blog.Exceptions
{
    public class CategoryNameAlreadyExistException : System.Exception
    {
        public CategoryNameAlreadyExistException(string Name) : base($"Category of Name {Name} already exist!") { }
    }
}
