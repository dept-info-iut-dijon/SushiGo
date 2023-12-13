using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using Logic_Layer.logic_exceptions;

namespace Logic_Layer;

/// <summary>
/// Représente une main à passer de joueur en joueur
/// </summary>
public class Hand
{
    private int id;
    private readonly List<Card> cards;
    
    /// <summary>
    /// Identifiant de la main
    /// </summary>
    public int Id => id;

    /// <summary>
    /// Liste des cartes de la main
    /// </summary>
    public List<Card> Cards => cards;

    /// <summary>
    /// Créée une main
    /// </summary>
    /// <param name="id">ID de la main</param>
    /// <param name="cards">Liste des cartes à mettre dans la main</param>
    public Hand(int id, List<Card> cards)
    {
        this.id = id;
        this.cards = cards;
    }

    /// <summary>
    /// Joue une carte dans la main
    /// </summary>
    /// <param name="card">La carte à jouer</param>
    /// <param name="board">Le plateau sur laquelle jouer la carte</param>
    /// <exception cref="CardNotInHandException">Lancée si la carte n'est pas dans la main</exception>
    public virtual void PlayCard(Card card, Board board)
    {
        Card? myCard = Contains(card);
        if (myCard is not null)
        {
            board.AddCard(myCard);
            RemoveCard(myCard);
        }
        else
        {
            throw new CardNotInHandException("La carte ne peut pas être jouée car elle n'est pas dans la main du joueur");
        }
    }

    /// <summary>
    /// Joue deux cartes à la fois grâce à une carte baguettes
    /// </summary>
    /// <param name="firstCard">Première carte à jouer</param>
    /// <param name="secondCard">Deuxième carte à jouer</param>
    /// <param name="board">Board où les cartes doivent être placées</param>
    /// <exception cref="CardNotInHandException">Lancée lorsque le joueur n'a pas </exception>
    public void PlayCard(Card firstCard, Card secondCard, Board board)
    {
        if (!board.CanPlayTwoCards)
            throw new NoChopstickInBoardException($"Le joueur avec la main {Id} ne peut pas jouer deux cartes sur ce board car il n'a pas de baguettes");
        
        Card? myFirstCard = Contains(firstCard);
        Card? mySecondCard = Contains(secondCard);

        if (myFirstCard is not null && mySecondCard is not null)
        {
            board.AddCard(myFirstCard);
            board.AddCard(mySecondCard);
            RemoveCard(myFirstCard);
            RemoveCard(mySecondCard);
            board.PlayChopstickCard();
            Cards.Add(new ChopstickCard());
        }
        else
        {
            throw new CardNotInHandException("Les cartes ne peuvent pas être jouées car au moins une n'est pas dans la main du joueur");
        }
    }

    // Retourne l'équivalent présent dans la main de la carte, renvoie null si elle n'est pas présente
    private Card? Contains(Card card)
    {
        return cards.Find(myCard => myCard.Equals(card));
    }
    
    /// <summary>
    /// Retire une carte de la main
    /// </summary>
    /// <param name="removedCard">Carte à retirer</param>
    private void RemoveCard(Card removedCard)
    {
        cards.Remove(removedCard);
    }
}