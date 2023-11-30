using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using LogicTest.datas_generators;
using LogicTest.datas_generators.Score;

namespace LogicTest.Score;

public class TempuraScoreCalculatorTests
{
    [Theory]
    [ClassData(typeof(TempuraScoreCalculatorDatasGenerator))]
    public void CalculateScore(List<Hand> hands, Dictionary<int, int> expectedScores)
    {
        Dictionary<int, int> actualScores = new TempuraScoreCalculator().CalculateScore(hands);
        Assert.Equal(expectedScores.Count, actualScores.Count);

        foreach (var hand in hands)
        {
            Assert.Equal(expectedScores[hands.IndexOf(hand)], actualScores[hand.Id]);
        }
        
    }
}