using Logic_Layer.cards;
using Logic_Layer.factories.card_factories;

namespace Logic_Layer.factories;

public class CardFactory
{
    private Dictionary<string, ISpecificCardFactory> specificCardFactories = new Dictionary<string, ISpecificCardFactory>()
    {
        { "chopstick", new ChopsticFactory() },
        { "dessert", new DessertFactory() },
        { "maki", new MakiFactory() },
        { "ravioli", new RavioliFactory() },
        { "sashimi", new SashimiFactory() },
        { "sushi", new SushiFactory() },
        { "tempura", new TempuraFactory() },
        { "wasabi", new WasabiFactory() }
    };

    /// <summary>
    /// Créée une liste de nouvelles instances de cartes
    /// </summary>
    /// <param name="parameters">Paramètres de création de la carte</param>
    /// <param name="quantity">Nombre de cartes a créer</param>
    /// <returns>Les instances générées</returns>
    public List<Card> CreateCard(string parameters, int quantity = 1)
    {
        List<Card> ret = new List<Card>();
        string key = parameters.Split(" ")[0];
        for (int i = 0; i < quantity; i++)
        {
            ret.Add(specificCardFactories[key].CreateCard(parameters));
        }

        return ret;
    }
}