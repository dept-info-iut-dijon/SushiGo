using Logic_Layer;
using Logic_Layer.IA.Factories;
using Logic_Layer.IA.Factories.IAFactories;
using Logic_Layer.IA.IAImplementation;
using LogicTest.datas_generators;

namespace LogicTest.IA.Factories
{
    public class DrunkedIAFactoryTests
    {
        [Theory]
        [ClassData(typeof(IAFactoryDatasGenerator))]
        public void CreateIA(int id, Board board, Hand hand)
        {
            ISpecificIAFactory iAFactory = new DrunkedIAFactory();

            Logic_Layer.IA.IA easyIA = iAFactory.CreateIA(id, board, hand);

            // Assert
            Assert.Equal($"IAFacile{id}", easyIA.Pseudo);
            Assert.Equal(hand, easyIA.Hand);
            Assert.Equal(typeof(DrunkenIA), easyIA.GetType());
        }
    }
}
