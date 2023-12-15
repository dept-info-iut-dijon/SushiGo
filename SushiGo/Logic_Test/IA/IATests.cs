using Logic_Layer;

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
