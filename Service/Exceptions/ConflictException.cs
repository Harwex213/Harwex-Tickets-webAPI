using System;

namespace Service.Exceptions
{
    public class ConflictException : ApplicationException
    {
        public ConflictException(string message) : base(message)
        {
        }
    }
}