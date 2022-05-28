namespace Core.Models.Exceptions;

public class DuplicateException : Exception
{
    public DuplicateException()
    {
    }

    public DuplicateException(string message) : base(message)
    {
    }
}