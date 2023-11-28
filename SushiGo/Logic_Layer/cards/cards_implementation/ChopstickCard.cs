namespace Logic_Layer.cards.cards_implementation;

/// <summary>
/// Carte baguette, permet au joueur de placer deux cartes quand il l'active
/// </summary>
public class ChopstickCard : Card, ISpecialCard
{
    private bool available;
    
    public override string Name => "Baguettes";


    public ChopstickCard()
    {
        available = true;
    }

    /// <summary>
    /// Utilise la carte, la rendant inutilisable par la suite
    /// </summary>
    public void Activate()
    {
        available = false;
    }

    public bool PlayerTurn()
    {
        return available;
    }

    public bool EndRound()
    {
        return false;
    }

    public override bool Equals(object? obj)
    {
        return obj is ChopstickCard card &&
               Name == card.Name &&
               available == card.available;
    }
}