using Logic_Layer.cards;

namespace Logic_Layer.factories;

/// <summary>
/// Factory construisant une implémentation spécifique de carte
/// </summary>
public interface ISpecificCardFactory
{
    /// <summary>
    /// Créée une instance de carte
    /// </summary>
    /// <param name="parameters">Paramètres de construction de la carte, séparés par des espaces</param>
    /// <returns>La carte créée</returns>
    Card CreateCard(string parameters);
}