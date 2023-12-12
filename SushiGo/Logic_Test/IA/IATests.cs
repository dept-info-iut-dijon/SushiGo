using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer.IA;
using Logic_Layer;
using Logic_Layer.IA.IAImplementation;

namespace LogicTest.IA
{
    public class IATests
    {
        [Fact]
        public void PlayerTurn_PlayCalled_WhenHavePlayedIsSet()
        {
            FakeIA IA = new FakeIA(3, new Board(), new Hand(3, new List<Logic_Layer.cards.Card>()), "toto");
            IA.PlayerTurn();
            Assert.True(IA.TestPlay);
        }
    }
}
