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
                List<SushiCard> sushiSalmonCards = SushiCardsSorterByType(sushiCards, SushiTypes.SALMON);
                List<SushiCard> sushiOmeletteCards = SushiCardsSorterByType(sushiCards, SushiTypes.OMELETTE);
                List<SushiCard> sushiCalamariCards = SushiCardsSorterByType(sushiCards, SushiTypes.CALAMARI);
                int formule = sushiOmeletteCards.Count * 1 + sushiSalmonCards.Count * 2 + sushiCalamariCards.Count * 3;
                score.Add(player.Id, formule);
            }
            return score;
        }

        private List<SushiCard> SushiCardsSorterByType(List<SushiCard> sushiCards, cards.SushiTypes sushiType) 
        { 
            List<SushiCard> sortedSushiCards = new List<SushiCard>(); 
            foreach (SushiCard card in sushiCards) { 
                if (card.Type == sushiType) 
                { 
                    sortedSushiCards.Add(card); 
                } 
            } 
            return sortedSushiCards; 
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
