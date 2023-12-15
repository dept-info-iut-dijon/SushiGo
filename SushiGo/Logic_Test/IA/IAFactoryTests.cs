using Logic_Layer;
using Logic_Layer.IA;
using Logic_Layer.IA.Factories;
using Logic_Layer.IA.IAImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTest.IA
{
    public class IAFactoryTests
    {
        [Fact]
        public void CreateIA_CreateIA_SameIA()
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
