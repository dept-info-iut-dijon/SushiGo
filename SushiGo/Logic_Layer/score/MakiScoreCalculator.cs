using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using Logic_Layer.cards.utils;

namespace Logic_Layer.score
{
    /// <summary>
    /// Calcule le score des cartes Makis d'une liste de cartes
    /// </summary>
    public class MakiScoreCalculator : IScoreCalculator
    {
        #region Attributes
        private int amountMakisFirst;
        private int amountMakisSecond;
        private List<Player> firstPlayerList;
        private List<Player> secondPlayerList;

        #endregion
        /// <summary>
        /// Constructeur du calculateur de score des cartes makis
        /// </summary>
        public MakiScoreCalculator()
        {
            this.amountMakisFirst = -1;
            this.amountMakisSecond = -1;
            this.firstPlayerList = new List<Player>();
            this.secondPlayerList = new List<Player>();
        }
        /// <summary>
        /// Calcule le score des cartes makis
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        public Dictionary<int, int> CalculateScore(List<Player> players)
        {
            int amountMakis;
            Dictionary<int, int> score = new Dictionary<int, int>();
            foreach (Player player in players)
            {
                score[player.Id] = 0;
                amountMakis = AmountMakisByPlayer(player.Board.Cards);
                FirstSecondRanking(player, amountMakis);
            }
            int scoreToGiveFirst = 6 / firstPlayerList.Count();
            foreach (Player player in firstPlayerList)
            {
                score[player.Id] = scoreToGiveFirst;
            }
            if (firstPlayerList.Count == 1)
            {
                if (secondPlayerList.Count != 0)
                {
                    int scoreToGiveSecond = 3 / secondPlayerList.Count();
                    foreach (Player player in secondPlayerList)
                    {
                        score[player.Id] = scoreToGiveSecond;
                    }
                }

            }

            return score;
        }


        #region Private Methodes
        private int AmountMakisByPlayer(List<Card> cards)
        {
            List<Card> makisCards = CardsSorter.TypeSort(typeof(MakiCard), cards);
            int amountMakis = 0;
            foreach (MakiCard makisCard in makisCards)
            {
                amountMakis += makisCard.Quantity;
            }
            return amountMakis;
        }

        private void FirstSecondRanking(Player player, int amountMakis)
        {
            if (amountMakis > amountMakisFirst)
            {
                NewFirstPlayer(player, amountMakis);
            }
            else if (amountMakis == amountMakisFirst)
            {
                AddFirstPlayer(player);
            }
            else if (amountMakis > amountMakisSecond && amountMakis != 0) //Si on a pas de makis on a pas de points
            {
                List<Player> secondPlayer = new List<Player>();
                secondPlayer.Add(player);
                NewSecondPlayers(secondPlayer, amountMakis);
            }
            else if (amountMakis == amountMakisSecond)
            {
                AddSecondPlayer(player);
            }
        }
        private void NewFirstPlayer(Player player, int amountMakis)
        {
            firstPlayerList.Clear();
            firstPlayerList.Add(player);
            amountMakisFirst = amountMakis;
        }
        private void AddFirstPlayer(Player player)
        {
            firstPlayerList.Add(player);
        }
        private void NewSecondPlayers(List<Player> players, int AmountMakis)
        {
            secondPlayerList.Clear();
            secondPlayerList = players;
            amountMakisSecond = AmountMakis;
        }
        private void AddSecondPlayer(Player player)
        {
            secondPlayerList.Add(player);
        }
    }
    #endregion
}
