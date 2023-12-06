using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.cards.utils
{
    public class CardsSorter
    {
        /// <summary>
        /// Trie les cartes et renvoie les cartes du type spécifié
        /// </summary>
        /// <param name="type">type dont on veut les cartes</param>
        /// <param name="cardsToSort">liste des cartes à trier</param>
        /// <returns></returns>
        public static List<Card> TypeSort(Type type, List<Card> cardsToSort)
        {
            List<Card> cardsSorted = new List<Card>();
            foreach(Card card in cardsToSort)
            {
                if (card.GetType() == type)
                {
                    cardsSorted.Add(card);
                }
            }
            return cardsSorted;
        }
    }
}
