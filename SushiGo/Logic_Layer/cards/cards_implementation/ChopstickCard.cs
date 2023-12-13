namespace Logic_Layer.cards.cards_implementation;

/// <summary>
/// Carte baguette, permet au joueur de placer deux cartes quand il l'active
/// </summary>
public class ChopstickCard : Card, ISpecialCard
{
    public override string Name => "Baguettes";


    public ChopstickCard()
    {
    }

    public bool PlayerTurn()
    {
        return true;
    }

    public bool EndRound()
    {
        return false;
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is ChopstickCard card &&
               Name == card.Name;
    }
}