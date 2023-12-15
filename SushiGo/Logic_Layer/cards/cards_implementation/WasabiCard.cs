using Logic_Layer.logic_exceptions;

namespace Logic_Layer.cards.cards_implementation;

/// <summary>
/// Carte wasabi, doit être associée à la première carte sushi à être placée par le joueur après qu'elle ait été placée
/// </summary>
public class WasabiCard : Card, ISpecialCard
{
    private SushiCard? sushi;

    /// <summary>
    /// Nom de la carte (Wasabi)
    /// </summary>
    public override string Name => "Wasabi";
    
    /// <summary>
    /// La carte sushi de la carte wasabi
    /// </summary>
    public SushiCard? Sushi => sushi;
    
    /// <summary>
    /// Constructeur de la carte wasabi
    /// </summary>
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

    /// <summary>
    /// Indique si le wasabi contient un sushi
    /// </summary>
    /// <returns>Un booleen représentant la présence d'un sushi</returns>
    public bool PlayerTurn()
    {
        return sushi is null;
    }

    /// <summary>
    /// Il n'y a rien de spécial à faire à la fin d'un tour
    /// </summary>
    /// <returns>False</returns>
    public bool EndRound()
    {
        return false;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is WasabiCard card &&
               Name == card.Name &&
               EqualityComparer<SushiCard?>.Default.Equals(sushi, card.sushi) &&
               EqualityComparer<SushiCard?>.Default.Equals(Sushi, card.Sushi);
    }
}