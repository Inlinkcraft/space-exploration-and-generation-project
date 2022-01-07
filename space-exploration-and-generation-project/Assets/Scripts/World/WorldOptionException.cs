using System;
using System.Runtime.Serialization;

[Serializable]
internal class WorldOptionException : Exception
{
    public WorldOptionException()
    {
    }

    public WorldOptionException(string message) : base(message)
    {
    }

    public WorldOptionException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected WorldOptionException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}