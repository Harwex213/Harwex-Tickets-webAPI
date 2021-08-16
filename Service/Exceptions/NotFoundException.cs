﻿using System;

namespace Service.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string message = "") : base(message)
        {
        }
    }
}