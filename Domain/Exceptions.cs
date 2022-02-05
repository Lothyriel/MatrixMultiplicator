using System.Runtime.Serialization;

namespace Domain.Exceptions
{
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
        public DesserializeException(string serializedString) : base($"Error tryng to desserialize {serializedString}")
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