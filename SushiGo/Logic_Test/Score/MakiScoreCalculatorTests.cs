using Logic_Layer;
using Logic_Layer.score;
using LogicTest.datas_generators.score;

namespace LogicTest.Score;

public class MakiScoreCalculatorTests
{
    [Theory]
    [ClassData(typeof(MakiScoreCalculatorDatasGenerator))]
    public void CalculateScore(List<Player> players, Dictionary<int, int> expectedScores)
    {
        var calculator = new MakiScoreCalculator();
        Dictionary<int, int> actualScores = calculator.CalculateScore(players);

        Assert.Equal(expectedScores, actualScores);
    }
}