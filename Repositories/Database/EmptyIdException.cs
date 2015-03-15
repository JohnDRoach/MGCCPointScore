namespace Repositories.Database
{
    using System;

    public class EmptyIdException : Exception
    {
        public EmptyIdException(string message) : base(message)
        {
        }
    }
}
