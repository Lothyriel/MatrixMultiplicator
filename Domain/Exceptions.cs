using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public class NoClientsConnected : Exception
    {
        public NoClientsConnected() : base("No Clients connected to the server")
        {
        }

        public NoClientsConnected(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected NoClientsConnected(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class InvalidMatrix : Exception
    {
        public InvalidMatrix() : base("Invalid Matrix, lines don't have the same number of elements")
        {
        }

        public InvalidMatrix(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidMatrix(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class DesserializeException : Exception
    {
        public DesserializeException(string serializedString) : base($"Error trying to desserialize {serializedString}")
        {
        }

        public DesserializeException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DesserializeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}