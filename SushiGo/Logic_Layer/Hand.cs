using Logic_Layer.cards;
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
            throw new CardNotInHandException(
                "La carte ne peut pas être jouée car elle n'est pas dans la main du joueur");
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