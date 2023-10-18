namespace Logic_Layer.logic_exceptions;

public class CardNotInHandException : Exception
{
    public CardNotInHandException(string? message) : base(message)
    {
    }
}