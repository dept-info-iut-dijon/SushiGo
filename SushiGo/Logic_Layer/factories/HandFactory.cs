using Logic_Layer.cards;
using Logic_Layer.logic_exceptions;

namespace Logic_Layer.factories;

/// <summary>
/// Génère les mains pour un round
/// </summary>
public class HandFactory
{
    #region Attributs
    private Random rand;
    private List<Card>? deck;
    
    // Stocke le nombre de carte par main selon le nombre de joueurs
    private static readonly Dictionary<int, int> playersToCardNumber = new Dictionary<int, int>()
    {
        { 2, 10 },
        { 3, 9 },
        { 3, 8 },
        { 4, 7 }
    };
    #endregion
    
    /// <summary>
    /// Constructeur de la factory
    /// </summary>
    public HandFactory()
    {
        rand = new Random();
    }

    /// <summary>
    /// Créée une main pour chaque joueur
    /// </summary>
    /// <param name="playersNumber">Nombre de joueurs pour qui les mains doivent être créées</param>
    /// <returns>Liste des mains à distribuer</returns>
    public List<Hand> CreateHands(int playersNumber)
    {
        // Initialisation des variables
        List<Hand> ret = new List<Hand>();
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

        // Répartition des mains
        for (var index = 0; index < tmpCards.Count; index++)
        {
            ret.Add(new Hand(index, tmpCards[index]));
        }

        return ret;
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

        throw new NotImplementedException("Il va falloir implémenter ici les appels à la factory des cartes");

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