using Logic_Layer.logic_exceptions;

namespace Logic_Layer.cards.cards_implementation;

/// <summary>
/// Carte wasabi, doit être associée à la première carte sushi à être placée par le joueur après qu'elle ait été placée
/// </summary>
public class WasabiCard : Card, ISpecialCard
{
    private SushiCard? sushi;
    
    public WasabiCard(string name) : base(name)
    {
        sushi = null;
    }

    /// <summary>
    /// Associe le wasabi à un sushi
    /// </summary>
    /// <param name="sushi">Le sushi auquel on veut associer le wasabi</param>
    public void AssociateSushi(SushiCard sushi)
    {
        if (this.sushi is null)
            this.sushi = sushi;
        else throw new ValueAlreadySetException("La carte wasabi est déjà associée à un sushi");
    }

    public bool PlayerTurn()
    {
        return sushi is null;
    }

    public bool EndRound()
    {
        return false;
    }
}