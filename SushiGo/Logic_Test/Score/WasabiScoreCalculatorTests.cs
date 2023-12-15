using Logic_Layer.score;
using Logic_Layer;
using LogicTest.datas_generators.score;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicTest.datas_generators.Score;

namespace LogicTest.Score
{
    public class WasabiScoreCalculatorTests
    {
        [Theory]
        [ClassData(typeof(WasabiScoreCalculatorDatasGenerator))]
        public void CalculateScore_WhenCalled_ReturnsScore(List<Player> players, Dictionary<int, int> expectedScores)
        {
            // Arrange
            var calculator = new WasabiScoreCalculator();
            Dictionary<int, int> actualScores = calculator.CalculateScore(players);

            // Assert
            Assert.Equal(expectedScores, actualScores);
        }
    }
}
