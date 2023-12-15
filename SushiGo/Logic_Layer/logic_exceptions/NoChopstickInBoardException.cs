namespace Logic_Layer.logic_exceptions;

public class NoChopstickInBoardException : Exception
{
    /// <summary>
    /// Il n'y a pas de baguette sur le plateau
    /// </summary>
    /// <param name="message">Message d'erreur</param>
    public NoChopstickInBoardException(string? message) : base(message)
    {
    }
}