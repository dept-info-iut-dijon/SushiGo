using Logic_Layer.cards;

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
    /// Retire une carte de la main
    /// </summary>
    /// <param name="removedCard">Carte à retirer</param>
    public void RemoveCard(Card removedCard)
    {
        cards.Remove(removedCard);
    }
}