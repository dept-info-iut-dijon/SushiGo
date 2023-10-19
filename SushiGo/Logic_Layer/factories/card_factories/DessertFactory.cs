using Logic_Layer.cards;

namespace Logic_Layer.factories.card_factories;

public class DessertFactory : ISpecificCardFactory
{
    public Card CreateCard(string parameters)
    {
        return new cards.cards_implementation.DessertCard();
    }
}