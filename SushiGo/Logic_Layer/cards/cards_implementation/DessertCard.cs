namespace Logic_Layer.cards.cards_implementation;

public class DessertCard : Card, ISpecialCard
{
    public DessertCard(string name) : base(name)
    {
    }

    public bool PlayerTurn()
    {
        return false;
    }

    public bool EndRound()
    {
        return true;
    }
}