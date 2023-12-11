namespace Logic_Layer.logic_exceptions;

public class NegativeScore : Exception
{
    /// <summary>
    /// Le score est négatif :
    /// </summary>
    /// <param name="message">Message de score négatif</param>
    public NegativeScore(string? message) : base(message)
    {
    }
}