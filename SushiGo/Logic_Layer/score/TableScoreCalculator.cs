using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.score
{
    /// <summary>
    /// Calcule le score de toute la table
    /// </summary>
    public class TableScoreCalculator
    {
        #region attributes
        private Table table;
        private List<IScoreCalculator> scoreCalculators;
        private Dictionary<int, int> scores;
        #endregion
        /// <summary>
        /// Constructeur du calculateur de la table 
        /// </summary>
        /// <param name="table"></param>
        public TableScoreCalculator(Table table)
        {
            this.table = table;
            this.scoreCalculators = new List<IScoreCalculator>();
            this.ListCalculatorInitializer();
            this.scores = new Dictionary<int, int>();
            this.PlayerInitializer();
        }
        /// <summary>
        /// Calcule le score de toute la table
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, int> CalculateScore()
        {
            foreach(IScoreCalculator scoreCalculator in scoreCalculators)
            {
                Dictionary<int, int> calculatedScore = scoreCalculator.CalculateScore(table.Players);
                foreach(Player player in table.Players)
                {
                    scores[player.Id] += calculatedScore[player.Id];
                }
            }
            return scores;
        }

        private void ListCalculatorInitializer()
        {
            scoreCalculators.Clear();
            scoreCalculators.Add(new MakiScoreCalculator());
            scoreCalculators.Add(new SushiScoreCalculator());
            scoreCalculators.Add(new SashimisScoreCalculator());
            scoreCalculators.Add(new TempuraScoreCalculator());
            scoreCalculators.Add(new RavioliScoreCalculator());
        }

        private void PlayerInitializer()
        {
            scores.Clear();
            foreach (Player player in table.Players)
            {
                scores[player.Id] = 0;
            }
        }

        /// <summary>
        /// Permet de récupérer le score d'un joueur
        /// </summary>
        /// <param name="player">joueur dont on veut le score</param>
        /// <returns>score entier</returns>
        public int GetScoreOfPlayer(Player player)
        {
            Dictionary<int, int> scores = CalculateScore();
            return scores[player.Id];
        }
    }
}
