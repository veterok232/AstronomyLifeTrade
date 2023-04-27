using System.Runtime.Serialization;

namespace ApplicationCore.Exceptions;

[Serializable]
public class InvalidInputException : Exception
{
    public InvalidInputException()
    {
    }

    public InvalidInputException(string message)
        : base(message)
    {
    }

    public InvalidInputException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected InvalidInputException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}