using Logic_Layer;
using Logic_Layer.score;
using LogicTest.datas_generators.score;

namespace LogicTest.Score;

public class SushiScoreCalculatorTests
{
    [Theory]
    [ClassData(typeof(SushiScoreCalculatorDatasGenerator))]
    public void CalculateScore_WhenCalled_ReturnsScore(List<Player> players, Dictionary<int, int> expectedScores)
    {
        // Arrange
        var calculator = new SushiScoreCalculator();
        Dictionary<int, int> actualScores = calculator.CalculateScore(players);

        // Assert
        Assert.Equal(expectedScores, actualScores);
    }
}