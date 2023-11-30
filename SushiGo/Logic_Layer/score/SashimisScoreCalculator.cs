using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using Logic_Layer.cards.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.score
{
    /// <summary>
    /// Classe qui calcule le score des cartes sashimis d'une liste de cartes
    /// </summary>
    public class SashimisScoreCalculator : IScoreCalculator
    {
        /// <summary>
        /// Calcule le score des cartes sashimis d'une liste de cartes
        /// </summary>
        /// <param name="players">les joueurs dont on veut le score</param>
        /// <returns>La liste des id des joueurs avec leurs scores</returns>
        public Dictionary<int, int> CalculateScore(List<Player> players)
        {
            Dictionary<int, int> ScoreSashimiList = new Dictionary<int, int>();
            foreach (Player player in players)
            {
                List<Card> sashimiCards = CardsSorter.TypeSort(typeof(SashimiCard), player.Board.Cards);
                ScoreSashimiList.Add(player.Id, ScoreSashimi(sashimiCards));
            }
            return ScoreSashimiList;
        }

        private int ScoreSashimi(List<Card> sashimiCards)
        {
            int score = 10*(sashimiCards.Count/3);
            return score;
        }
    }
}
