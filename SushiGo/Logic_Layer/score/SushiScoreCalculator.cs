using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using Logic_Layer.cards.utils;

namespace Logic_Layer.score
{
    /// <summary>
    /// Classe qui calcule le score des cartes sushi d'une liste de cartes
    /// </summary>
    public class SushiScoreCalculator : IScoreCalculator
    {
        /// <summary>
        /// Constructeur du calculateur de score des cartes sushis
        /// </summary>
        public SushiScoreCalculator()
        {
        }

        /// <summary>
        /// Calcule le score des cartes sushis d'une liste de cartes
        /// </summary>
        /// <param name="players">les joueurs dont on veut le score</param>
        /// <returns>La liste des id des joueurs avec leurs scores</returns>
        public Dictionary<int, int> CalculateScore(List<Player> players)
        {
            Dictionary<int, int> score = new Dictionary<int, int>();
            foreach (Player player in players)
            {
                List<Card> sushiCardsBeforeConversion = CardsSorter.TypeSort(typeof(SushiCard), player.Board.Cards);
                List<SushiCard> sushiCards = ListCardToListSushiCardConverter(sushiCardsBeforeConversion);
                List<SushiCard> sushiSalmonCards = SushiSalmonCardsSorter(sushiCards);
                List<SushiCard> sushiOmeletteCards = SushiOmeletteCardsSorter(sushiCards);
                List<SushiCard> sushiCalamariCards = SushiCalamariCardsSorter(sushiCards);
                int formule = sushiOmeletteCards.Count * 1 + sushiSalmonCards.Count * 2 + sushiCalamariCards.Count * 3;
                score.Add(player.Id, formule);
            }
            return score;
            
        }

        private List<SushiCard> SushiSalmonCardsSorter(List<SushiCard> sushiCards)
        {
            List<SushiCard> sushiSalmonCards = new List<SushiCard>();
            foreach(SushiCard card in sushiCards)
            {
                if (card.Type == cards.SushiTypes.SALMON)
                {
                    sushiSalmonCards.Add(card);
                }
            }
            return sushiSalmonCards;
        }

        private List<SushiCard> SushiOmeletteCardsSorter(List<SushiCard> sushiCards)
        {
            List<SushiCard> sushiOmeletteCards = new List<SushiCard>();
            foreach (SushiCard card in sushiCards)
            {
                if (card.Type == cards.SushiTypes.OMELETTE)
                {
                    sushiOmeletteCards.Add(card);
                }
            }
            return sushiOmeletteCards;
        }

        private List<SushiCard> SushiCalamariCardsSorter(List<SushiCard> sushiCards)
        {
            List<SushiCard> sushiCalamariCards = new List<SushiCard>();
            foreach (SushiCard card in sushiCards)
            {
                if (card.Type == cards.SushiTypes.CALAMARI)
                {
                    sushiCalamariCards.Add(card);
                }
            }
            return sushiCalamariCards;
        }

        private List<SushiCard> ListCardToListSushiCardConverter(List<Card> cards)
        {
            List<SushiCard> sushiCards = new List<SushiCard>();
            foreach (Card card in cards)
            {
                sushiCards.Add((SushiCard)card);
            }
            return sushiCards;
        }
    }
}
