using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using Logic_Layer.cards.utils;
using Logic_Layer.logic_exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.score
{
    /// <summary>
    /// Classe qui calcule le score des cartes raviolis d'une liste de cartes
    /// </summary>
    public class RavioliScoreCalculator : IScoreCalculator
    {
        /// <summary>
        /// Constructeur du calculateur de score des cartes raviolis
        /// </summary>
        public RavioliScoreCalculator()
        {
        }

        /// <summary>
        /// Calcule le score des cartes raviolis d'une liste de cartes
        /// </summary>
        /// <param name="players">les joueurs dont on veut le score</param>
        /// <returns>La liste des id des joueurs avec leurs scores</returns>
        public Dictionary<int, int> CalculateScore(List<Player> players)
        {
            Dictionary<int, int> ScoreRaviolisList = new Dictionary<int, int>();
            foreach (Player player in players)
            {
                List<Card> raviolisCards = new List<Card>();
                raviolisCards = CardsSorter.TypeSort(typeof(RavioliCard), player.Board.Cards);
                ScoreRaviolisList.Add(player.Id, ScoreRaviolis(raviolisCards));
            }
            return ScoreRaviolisList;
        }

        private int ScoreRaviolis(List<Card> cards)
        {
            int score = 0;
            switch (cards.Count())
            {
                case 0:score = 0; break;
                case 1:score = 1; break;
                case 2:score = 3; break;
                case 3:score = 6; break;
                case 4:score = 10; break;
                case >=5:score = 15; break;
                default:throw new NegativeScore("Le score est négatif");
            }
            return score;
        }
    }
}
