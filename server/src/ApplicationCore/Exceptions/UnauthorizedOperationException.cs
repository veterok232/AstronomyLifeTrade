using System.Runtime.Serialization;

namespace ApplicationCore.Exceptions;

[Serializable]
public class UnauthorizedOperationException : Exception
{
    public UnauthorizedOperationException()
    {
    }

    public UnauthorizedOperationException(string message)
        : base(message)
    {
    }

    public UnauthorizedOperationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected UnauthorizedOperationException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}