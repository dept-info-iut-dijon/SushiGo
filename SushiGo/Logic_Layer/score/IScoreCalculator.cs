namespace Logic_Layer.score
{
    /// <summary>
    /// Interface de calculateur de score
    /// </summary>
    public interface IScoreCalculator
    {
        /// <summary>
        /// Calcule le score d'une liste de cartes
        /// </summary>
        /// <param name="playerBoards">list des boards dont on veut le score</param>
        /// <returns>L'id de chaque joueur associé à son score dans le dictionnaire</returns>
        public Dictionary<int, int> CalculateScore(List<Player> players);
    }
}
