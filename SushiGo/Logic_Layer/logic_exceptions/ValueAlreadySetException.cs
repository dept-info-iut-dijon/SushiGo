namespace Logic_Layer.logic_exceptions;

public class ValueAlreadySetException : Exception
{
    public ValueAlreadySetException(string? message) : base(message)
    {
    }
}