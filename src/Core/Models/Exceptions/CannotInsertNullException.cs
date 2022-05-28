namespace Core.Models.Exceptions;

public class CannotInsertNullException : Exception
{
    public CannotInsertNullException()
    {
    }

    public CannotInsertNullException(string message) : base(message)
    {
    }
}