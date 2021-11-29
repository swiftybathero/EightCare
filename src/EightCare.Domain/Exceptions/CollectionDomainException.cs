using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace EightCare.Domain.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class CollectionDomainException : Exception
    {
        public CollectionDomainException()
        {
        }

        public CollectionDomainException(string message) : base(message)
        {
        }

        public CollectionDomainException(string message, Exception inner) : base(message, inner)
        {
        }

        protected CollectionDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
