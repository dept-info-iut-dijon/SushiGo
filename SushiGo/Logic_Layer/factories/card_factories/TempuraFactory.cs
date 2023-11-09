using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;

namespace Logic_Layer.factories.card_factories;

public class TempuraFactory : ISpecificCardFactory
{
    public Card CreateCard(string parameters)
    {
        return new TempuraCard();
    }
}