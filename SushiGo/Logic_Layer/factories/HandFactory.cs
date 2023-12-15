using Logic_Layer.cards;
using Logic_Layer.factories.utils;
using Logic_Layer.logic_exceptions;

namespace Logic_Layer.factories;

/// <summary>
/// Génère les mains pour un round
/// </summary>
public class HandFactory
{
    #region Attributs
    private readonly Random rand;
    private List<Card>? deck;
    private readonly CardFactory factory;

    // Stocke le nombre de carte par main selon le nombre de joueurs
    private static readonly Dictionary<int, int> playersToCardNumber = new()
    {
        { 2, 10 },
        { 3, 9 },
        { 4, 8 },
        { 5, 7 }
    };
    #endregion

    /// <summary>
    /// Constructeur de la factory
    /// </summary>
    public HandFactory()
    {
        rand = new Random();
        factory = new CardFactory();
    }

    /// <summary>
    /// Créée une main pour chaque joueur
    /// </summary>
    /// <param name="playersNumber">Nombre de joueurs pour qui les mains doivent être créées</param>
    /// <returns>Liste des mains à distribuer</returns>
    public List<Hand> CreateHands(int playersNumber)
    {
        if (playersNumber is < 2 or > 5)
            throw new WrongPlayersNumberException("Le nombre de joueur devrait être inclus entre 2 et 5");

        // Initialisation des variables
        List<List<Card>> tmpCards = new List<List<Card>>();
        deck = CreateDeck();
        for (int i = 0; i < playersNumber; i++)
        {
            tmpCards.Add(new List<Card>());
        }

        // Distribution des cartes
        for (int i = 0; i < playersToCardNumber[playersNumber]; i++)
        {
            for (int j = 0; j < playersNumber; j++)
            {
                GiveCardToList(tmpCards, j);
            }
        }

        // Répartition des mains et retour
        return tmpCards.Select((t, index) => new Hand(index, t)).ToList();
    }

    #region Méthodes privées
    // Ajoute une carte du deck au hasard à une liste puis la retire du deck 
    private void GiveCardToList(List<List<Card>> tmpCards, int index)
    {
        Card c = PickRandomCard();
        tmpCards[index].Add(c);
        if (deck != null) deck.Remove(c);
        else throw new DeckNotSetException("Le deck n'est pas encore initialisé");
    }

    // Initialise le deck
    private List<Card> CreateDeck()
    {
        List<Card> ret = new List<Card>();

        // Récupération des données de génération
        var generationDatas = new GenerationParametersExtractor().GetParameters();

        // On lance chacune des générations et on les ajoute au résultat
        foreach (var param in generationDatas.Keys)
        {
            ret.AddRange(factory.CreateCard(param, generationDatas[param]));
        }

        return ret;
    }

    // Retourne une carte aléatoire parmi celles présentes dans le deck
    private Card PickRandomCard()
    {
        if (deck is not null)
            return deck[rand.Next(0, deck.Count)];
        throw new DeckNotSetException("Le deck n'a pas encore été initialisé");
    }
    #endregion

}