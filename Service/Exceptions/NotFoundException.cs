using System;

namespace Service.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message = "Not Found") : base(message)
        {
        }
    }
}