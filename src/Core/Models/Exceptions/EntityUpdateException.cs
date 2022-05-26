namespace Core.Models.Exceptions;

public class EntityUpdateException : Exception
{
    public EntityUpdateException()
    {
    }

    public EntityUpdateException(string message) : base(message)
    {
    }
}