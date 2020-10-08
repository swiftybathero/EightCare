using System;
using System.Diagnostics.CodeAnalysis;

namespace EightCare.Domain.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class KeeperDomainException : Exception
    {
        public KeeperDomainException()
        {
        }

        public KeeperDomainException(string message) : base(message)
        {
        }

        public KeeperDomainException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
