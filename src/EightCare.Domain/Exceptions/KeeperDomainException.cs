using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace EightCare.Domain.Exceptions
{
    [Serializable]
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

        protected KeeperDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
