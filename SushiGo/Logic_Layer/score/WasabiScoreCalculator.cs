using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using Logic_Layer.cards.utils;
using Logic_Layer.score.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.score
{
    public class WasabiScoreCalculator : IScoreCalculator
    {
        /// <summary>
        /// Contructeur du calculateur des scores des cartes wasabi
        /// </summary>
        public WasabiScoreCalculator()
        {

        }

        /// <inheritdoc/>
        /// Une carte Wasabi triple le score d'une carte sushi
        public Dictionary<int, int> CalculateScore(List<Player> players)
        {
            Dictionary<int, int> ScoreWasabisList = new Dictionary<int, int>();
            foreach (Player player in players)
            {
                int scorePlayer = 0;
                ScoreSushiByType sushiScore = new ScoreSushiByType();
                List<Card> wasabiCards = CardsSorter.TypeSort(typeof(WasabiCard), player.Board.Cards);
                foreach(WasabiCard wasabiCard in wasabiCards)
                {
                    if (wasabiCard.Sushi != null)
                    {
                        scorePlayer -= sushiScore.ScoreBySushiTypes[wasabiCard.Sushi.Type];
                        scorePlayer += 3*sushiScore.ScoreBySushiTypes[wasabiCard.Sushi.Type];
                    }
                }
                ScoreWasabisList.Add(player.Id, scorePlayer);
            }
            return ScoreWasabisList;
        }
    }
}
