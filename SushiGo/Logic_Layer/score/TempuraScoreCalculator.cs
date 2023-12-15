using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using Logic_Layer.cards.utils;

namespace Logic_Layer.score
{
    /// <summary>
    /// Classe qui calcule le score des cartes Tempura d'une liste de cartes
    /// </summary>
    public class TempuraScoreCalculator : IScoreCalculator
    {
        /// <summary>
        /// Constructeur du calculateur de score des cartes tempura
        /// </summary>
        public TempuraScoreCalculator()
        {
        }

        /// <summary>
        /// Calcule le score des cartes tempura d'une liste de cartes
        /// </summary>
        /// <param name="players">les joueurs dont on veut le score</param>
        /// <returns>La liste des id des joueurs avec leurs scores</returns>
        public Dictionary<int, int> CalculateScore(List<Player> players)
        {
            Dictionary<int, int> ScoreTempuraList = new Dictionary<int, int>();
            foreach (Player player in players)
            {
                List<Card> tempuraCards = CardsSorter.TypeSort(typeof(TempuraCard), player.Board.Cards);
                ScoreTempuraList.Add(player.Id, ScoreTempura(tempuraCards));
            }
            return ScoreTempuraList;
        }

        private int ScoreTempura(List<Card> tempuraCards)
        {
            int score = 5 * (tempuraCards.Count / 2);
            return score;
        }

    }
}
