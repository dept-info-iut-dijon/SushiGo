using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using Logic_Layer.logic_exceptions;

namespace Logic_Layer.factories.card_factories;

public class MakiFactory : ISpecificCardFactory
{
    /// <summary>
    /// Créée une instance de carte Maki
    /// </summary>
    /// <param name="parameters">Nombre de makis à placer sur la carte -> ]0;3]</param>
    /// <returns>L'instance créée</returns>
    /// <exception cref="InvalidParameterException">Lancée si le nombre de makis est invalide</exception>
    public Card CreateCard(string parameters)
    {
        string stringedQuantity = parameters.Split(" ")[1];
        int quantity = int.Parse(stringedQuantity);
        if (quantity is > 0 and <= 3)
            return new MakiCard(quantity);
        throw new InvalidParameterException("Le nombre de makis est invalide, il ne peut y en avoir qu'entre 1 et 3 sur la carte");
    }
}