namespace Logic_Layer.cards.cards_implementation;

public class DessertCard : Card, ISpecialCard
{
    public override string Name => "Carte Dessert";
    
    public DessertCard()
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