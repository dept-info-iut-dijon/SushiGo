namespace Logic_Layer.cards.cards_implementation;

public class DessertCard : Card, ISpecialCard
{
    public override string Name => "Dessert";

    public bool PlayerTurn()
    {
        return false;
    }

    public bool EndRound()
    {
        return true;
    }

    public override bool Equals(object? obj)
    {
        return obj is DessertCard card &&
               Name == card.Name;
    }
}