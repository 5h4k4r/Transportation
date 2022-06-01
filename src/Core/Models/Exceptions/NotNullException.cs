namespace Core.Models.Exceptions;

public class NotNullException : Exception
{
    public NotNullException()
    {
    }

    public NotNullException(string message) : base(message)
    {
    }
}