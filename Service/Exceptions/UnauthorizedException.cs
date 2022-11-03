using System;

namespace Service.Exceptions
{
    public class UnauthorizedException : ApplicationException
    {
        public UnauthorizedException(string message = "") : base(message)
        {
        }
    }
}