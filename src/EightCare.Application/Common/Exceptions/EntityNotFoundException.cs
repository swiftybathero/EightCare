using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace EightCare.Application.Common.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
