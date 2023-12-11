using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using Logic_Layer.score;
using LogicTest.datas_generators;
using LogicTest.datas_generators.score;

namespace LogicTest.Score;

public class TempuraScoreCalculatorTests
{
    [Theory]
    [ClassData(typeof(TempuraScoreCalculatorDatasGenerator))]
    public void CalculateScore(List<Player> players, Dictionary<int, int> expectedScores)
    {
        Dictionary<int, int> actualScores = new TempuraScoreCalculator().CalculateScore(players);
        Assert.Equal(expectedScores.Count, actualScores.Count);

        foreach (var player in players)
        {
            Assert.Equal(expectedScores[player.Id], actualScores[player.Id]);
        }
    }
}