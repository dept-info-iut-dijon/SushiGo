using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using Logic_Layer.cards.utils;

namespace Logic_Layer.factories.card_factories;

public class SushiFactory : ISpecificCardFactory
{
    /// <summary>
    /// Créée une instance de carte sushi
    /// </summary>
    /// <param name="parameters">Type de sushi à créer</param>
    /// <returns>L'instance créée</returns>
    /// <seealso cref="SushiTypesConverter"/>
    public Card CreateCard(string parameters)
    {
        string stringedSushi = parameters.Split(" ")[1];
        return new SushiCard(new SushiTypesConverter().StringToSushi(stringedSushi));
    }
}