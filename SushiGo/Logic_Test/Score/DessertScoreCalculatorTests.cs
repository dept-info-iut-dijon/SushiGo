using Logic_Layer;
using Logic_Layer.score;
using LogicTest.datas_generators.score;

namespace LogicTest.Score;

public class DessertScoreCalculatorTests
{
    [Theory]
    [ClassData(typeof(DessertScoreCalculatorDatasGenerator))]
    public void CalculateScore(List<Player> players, Dictionary<int, int> expectedScores)
    {
        var calculator = new DessertScoreCalculator();
        Dictionary<int, int> actualScores = calculator.CalculateScore(players);
        
        Assert.Equal(expectedScores, actualScores);
    }
}