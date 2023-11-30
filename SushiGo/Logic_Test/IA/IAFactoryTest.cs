using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.IA;

namespace LogicTest.IA
{
    public class IAFactoryTest
    {
        [Fact]
        public void CreateIA_Easy()
        {
            IAFactory iAFactory = new IAFactory();

            Board board = new Board();
            Hand hand = new Hand(3, new List<Card>());
            Logic_Layer.IA.IA easyIA = iAFactory.CreateIA(IADifficultyEnum.FACILE, 3, board, hand);

            // Assert
            Assert.Equal("IAFacile3", easyIA.Pseudo);
            Assert.Equal(hand, easyIA.Hand);
        }
    }
}
