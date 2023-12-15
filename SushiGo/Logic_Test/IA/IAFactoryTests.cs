using Logic_Layer.IA.Factories;
using Logic_Layer.IA.IAImplementation;
namespace LogicTest.IA
{
    public class IAFactoryTests
    {
        [Fact]
        public void CreateIA_CreateSevendDrunkedIA()
        {
            // Arrange
            IAFactory iAFactory = new IAFactory();

            // Act
            List<Logic_Layer.IA.IA> ias = iAFactory.CreateIA(Enumerable.Repeat<string>("DrunkedIA", 7).ToArray());

            // Assert
            Assert.Equal(7, ias.Count);
            Assert.Equal(typeof(DrunkenIA), ias[0].GetType());
        }
    }
}
