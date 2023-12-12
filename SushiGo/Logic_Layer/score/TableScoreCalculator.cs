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

        private readonly Table table;
        private readonly List<IScoreCalculator> scoreCalculators;
        private readonly List<IScoreCalculator> endGameScoreCalculators;
        private readonly Dictionary<int, int> scores;

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
            this.endGameScoreCalculators = new List<IScoreCalculator> { new DessertScoreCalculator() };
            this.scores = new Dictionary<int, int>();
            this.PlayerInitializer();
        }
        
        #region Méthodes publiques

        /// <summary>
        /// Calcule le score de toute la table
        /// </summary>
        /// <returns>Dictionnaire des scores triés par ID des joueurs</returns>
        public Dictionary<int, int> CalculateScore()
        {
            foreach (IScoreCalculator scoreCalculator in scoreCalculators)
            {
                Dictionary<int, int> calculatedScore = scoreCalculator.CalculateScore(table.Players);
                foreach (Player player in table.Players)
                {
                    scores[player.Id] += calculatedScore[player.Id];
                }
            }

            // Pas besoin d'aller plus loin si on en est pas à la dernière manche
            // On aurait pu faire la même chose en mettant le foreach dans une condition mais ça augmenterait le nesting
            if (table.RoundNumber <3) return scores;

            var endGameScore = EndGameScore();
            foreach (var player in table.Players)
            {
                scores[player.Id] += endGameScore[player.Id];
            }

            return scores;
        }
        
        
        /// <summary>
        /// Permet de récupérer le score d'un joueur
        /// </summary>
        /// <param name="player">joueur dont on veut le score</param>
        /// <returns>score entier</returns>
        public int GetScoreOfPlayer(Player player)
        {
            return scores[player.Id];
        }
        #endregion
        
        #region Méthodes privées

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

        private Dictionary<int, int> EndGameScore()
        {
            Dictionary<int, int> endGameScore = new Dictionary<int, int>();
            foreach (Player player in table.Players)
            {
                endGameScore[player.Id] = 0;
            }
            
            foreach (IScoreCalculator scoreCalculator in endGameScoreCalculators)
            {
                Dictionary<int, int> calculatedScore = scoreCalculator.CalculateScore(table.Players);
                foreach (Player player in table.Players)
                {
                    endGameScore[player.Id] += calculatedScore[player.Id];
                }
            }
            
            return endGameScore;
        }

        #endregion
    }
}