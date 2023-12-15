using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using Logic_Layer.cards.utils;

namespace Logic_Layer;

/// <summary>
/// Représente les cartes posées sur la table devant le joueur
/// </summary>
public class Board
{
    private List<Card> cards = new();

    /// <summary>
    /// Liste des cartes du joueur
    /// </summary>
    public List<Card> Cards
    {
        get => cards;
        private set => cards = value;
    }

    /// <summary>
    /// Effectue les actions nécessaires au début du tour du joueur possédant la main
    /// </summary>
    /// <see cref="ISpecialCard"/>
    /// <returns>Les cartes spéciales à prendre en compte</returns>
    public virtual List<ISpecialCard> PlayerTurn()
    {
        List<ISpecialCard> ret = new List<ISpecialCard>();
        
        // On rassemble les cartes spéciales devant être prises en compte
        foreach (Card c in Cards)
        {
            if (c is ISpecialCard card && card.PlayerTurn())
                ret.Add(card);
        }
        
        return ret;
    }

    /// <summary>
    /// Effectue les actions nécessaires à la fin de la manche
    /// </summary>
    /// <returns></returns>
    public virtual List<ISpecialCard> EndRound()
    {
        var ret = new List<ISpecialCard>();

        // On rassemble les cartes spéciales devant être prises en compte
        foreach (var c in Cards)
        {
            if (c is ISpecialCard card && card.EndRound())
                ret.Add(card);
        }
        
        Cards = ret.Select(c => (Card) c).ToList();

        return ret;
    }

    /// <summary>
    /// Ajoute une carte sur le plateau
    /// Fait une vérification de la carte Wasabi si la carte est un sushi.
    /// </summary>
    /// <param name="card">Carte à ajouter</param>
    public void AddCard(Card card)
    {
        if(card is SushiCard)
        {
            List<Card> wasabiCards = CardsSorter.TypeSort(typeof(WasabiCard), cards);
            bool sushiIsAssociated = false;
            sushiIsAssociated = IsSushiAssociatedWithWasabi(wasabiCards, card);
            if (!sushiIsAssociated)
            {
                cards.Add(card);
            }
        }
        else
        {
            cards.Add(card);
        }
    }
    private bool IsSushiAssociatedWithWasabi(List<Card> wasabiCards, Card card)
    {
        bool sushiIsAssociated = false;
        foreach (WasabiCard c in wasabiCards)
        {
            if (c.Sushi is null && !sushiIsAssociated)
            {
                c.AssociateSushi(card as SushiCard);
                sushiIsAssociated = true;
            }
        }
        return sushiIsAssociated;
    }

}