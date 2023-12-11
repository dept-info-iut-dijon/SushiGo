using Logic_Layer;
using Logic_Layer.score;
using LogicTest.datas_generators.score;

namespace LogicTest.Score;

public class RavioliScoreCalculatorTests
{
    [Theory]
    [ClassData(typeof(RavioliScoreCalculatorDatasGenerator))]
    public void CalculateScore_WhenGivenValidInput_ReturnsCorrectScore(List<Player> players, Dictionary<int, int> expectedScore)
    {
        var calculator = new RavioliScoreCalculator();

        Dictionary<int, int> actualScore = calculator.CalculateScore(players);
        
        Assert.Equal(expectedScore, actualScore);
    }
}