using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic_Layer.IA;
using Logic_Layer;

namespace LogicTest.IA
{
    public class IATests
    {
        [Fact]
        public void HavePlayed_PlayCalled_WhenHavePlayedIsSet()
        {
            var iaMock = new Mock<Logic_Layer.IA.IA>(3, new Board(), new Hand(3, new List<Logic_Layer.cards.Card>()), "bloup");
            Logic_Layer.IA.IA mocked = iaMock.Object;
            mocked.PlayerTurn();
            iaMock.Verify(x => x.Play(), Times.Once);
        }
    }
}
