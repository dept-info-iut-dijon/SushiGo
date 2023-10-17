using Logic_Layer.logic_exceptions;

namespace Logic_Layer.cards.cards_implementation;

/// <summary>
/// Carte wasabi, doit être associée à la première carte sushi à être placée par le joueur après qu'elle ait été placée
/// </summary>
public class WasabiCard : Card, ISpecialCard
{
    private SushiCard? sushi;
    
    public override string Name => "Wasabi";

    public WasabiCard()
    {
        sushi = null;
    }

    /// <summary>
    /// Associe le wasabi à un sushi
    /// </summary>
    /// <param name="sushiToAssociate">Le sushi auquel on veut associer le wasabi</param>
    public void AssociateSushi(SushiCard sushiToAssociate)
    {
        if (this.sushi is null)
            this.sushi = sushiToAssociate;
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