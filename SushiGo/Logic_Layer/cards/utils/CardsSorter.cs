using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.cards.utils
{
    public class CardsSorter
    {
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
