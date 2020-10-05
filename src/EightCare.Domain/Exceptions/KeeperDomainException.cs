using System;

namespace EightCare.Domain.Exceptions
{
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
