namespace Logic_Layer.logic_exceptions;

public class WrongPlayersNumberException : Exception
{
    public WrongPlayersNumberException(string? message) : base(message)
    {
    }
}