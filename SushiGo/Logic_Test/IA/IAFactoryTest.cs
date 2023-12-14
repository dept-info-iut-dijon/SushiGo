using Logic_Layer;
using Logic_Layer.IA;
using Logic_Layer.IA.Factories;
using Logic_Layer.IA.IAImplementation;
using LogicTest.datas_generators;

namespace LogicTest.IA
{
    public class IAFactoryTest
    {
        [Theory]
        [ClassData(typeof(IAFactoryDatasGenerator))]
        public void CreateIA_Easy(IADifficultyEnum difficulty, int id, Board board, Hand hand)
        {
            IIAFactory iAFactory = new EasyIAFactory();

            Logic_Layer.IA.IA easyIA = iAFactory.CreateIA(3, board, hand);

            // Assert
            Assert.Equal("IAFacile3", easyIA.Pseudo);
            Assert.Equal(hand, easyIA.Hand);
            Assert.Equal(typeof(DrunkenIA), easyIA.GetType());
        }
    }
}
