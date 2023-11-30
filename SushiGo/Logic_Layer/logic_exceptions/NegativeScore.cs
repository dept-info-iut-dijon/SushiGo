namespace Logic_Layer.logic_exceptions;

public class NegativeScore : Exception
{
    public NegativeScore(string? message) : base(message)
    {
    }
}