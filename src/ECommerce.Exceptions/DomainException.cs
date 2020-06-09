using System;
using System.Collections.Generic;

namespace ECommerce.Exceptions
{
    public class DomainException : Exception
    {
        public IEnumerable<string> Errors { get; private set; }

        public DomainException()
        {
        }

        public DomainException(string message) : base(message)
        {
        }

        public DomainException(string message, IEnumerable<string> errors) : base(message)
        {
            Errors = errors;
        }
    }
}
