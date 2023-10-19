namespace Logic_Layer.logic_exceptions;

/// <summary>
/// Lancée quand un paramètre invalide est passé à une fonction
/// </summary>
public class InvalidParameterException : Exception
{
    public InvalidParameterException(string? message) : base(message)
    {
    }
}