namespace Logic_Layer.logic_exceptions;

public class DeckNotSetException : Exception
{
    public DeckNotSetException(string? message) : base(message)
    {
    }
}