using System.Runtime.Serialization;

namespace ApplicationCore.Exceptions;

[Serializable]
public class PotentiallyConcurrentModificationsException : Exception
{
    public PotentiallyConcurrentModificationsException()
    {
    }

    public PotentiallyConcurrentModificationsException(string message)
        : base(message)
    {
    }

    public PotentiallyConcurrentModificationsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected PotentiallyConcurrentModificationsException(
        SerializationInfo serializationInfo,
        StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
}