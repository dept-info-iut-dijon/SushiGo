using Logic_Layer;
using LogicTest.datas_generators.score;
using Logic_Layer.score;

namespace LogicTest.Score;

public class SashimiScoreCalculatorTests
{
    [Theory]
    [ClassData(typeof(SashimiScoreCalculatorDatasGenerator))]
    public void CalculateScore_ReturnCorrectScore(List<Player> hands, Dictionary<int, int> expectedScore)
    {
        var calculator = new SashimiScoreCalculator();
        
        Dictionary<int, int> actualScore = calculator.CalculateScore(hands);
        
        Assert.Equal(expectedScore, actualScore);
    }
}