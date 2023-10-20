namespace Logic_Layer.logic_exceptions;

public class SushiTypeDoesNotExist : Exception
{
    public SushiTypeDoesNotExist(string? message) : base(message)
    {
    }
}