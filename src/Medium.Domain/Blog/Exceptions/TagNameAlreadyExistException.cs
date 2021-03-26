using System;

namespace Medium.Domain.Blog.Exceptions
{
    public class TagNameAlreadyExistException : System.Exception
    {
        public TagNameAlreadyExistException(string Name) : base($"Tag of Name {Name} already exist!") { }
    }
}
