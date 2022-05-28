namespace Core.Models.Exceptions;

public class MaxLengthExceededException : Exception
{
    public MaxLengthExceededException()
    {
    }

    public MaxLengthExceededException(string message) : base(message)
    {
    }
}