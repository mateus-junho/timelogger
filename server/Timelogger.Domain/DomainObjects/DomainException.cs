using System;

namespace Timelogger.Domain.DomainObjects
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {

        }
    }
}
