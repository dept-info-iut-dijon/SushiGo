namespace Logic_Layer.cards;

/// <summary>
/// Les cartes spéciales crééent des exceptions aux règles du jeu
/// </summary>
public interface ISpecialCard
{
    /// <summary>
    /// Appelé au début du tour du joueur possédant la carte
    /// </summary>
    /// <returns>Indique si la carte peut être prise en compte</returns>
    bool PlayerTurn();

    /// <summary>
    /// Appelé à la fin de la manche
    /// </summary>
    /// <returns>Indique si la carte peut être prise en compte</returns>
    bool EndRound();
}