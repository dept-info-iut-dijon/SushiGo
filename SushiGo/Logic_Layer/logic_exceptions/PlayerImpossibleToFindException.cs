namespace Logic_Layer.logic_exceptions;

public class PlayerImpossibleToFindException : Exception
{
    public PlayerImpossibleToFindException(string? message) : base(message)
    {
    }
}