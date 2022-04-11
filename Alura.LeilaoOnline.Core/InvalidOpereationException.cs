using System;
using System.Runtime.Serialization;

namespace Alura.LeilaoOnline.Core
{
    [Serializable]
    internal class InvalidOpereationException : Exception
    {
        public InvalidOpereationException()
        {
        }

        public InvalidOpereationException(string message) : base(message)
        {
        }

        public InvalidOpereationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidOpereationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}