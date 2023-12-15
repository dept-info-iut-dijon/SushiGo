using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using Logic_Layer.IA.IAImplementation;

namespace LogicTest.IA.IaImplementation;

public class DrunkenIATests
{
    [Fact]
    public void Play_DoesNotClearBoard()
    {
        DrunkenIA ia = new(0, new Board(), new Hand(0, new List<Card>() { new ChopstickCard(), new DessertCard(), new MakiCard(2) }), "DrunkenIA");
        
        int boardCount = ia.Board.Cards.Count;
        ia.PlayerTurn();
        Assert.Equal(++boardCount, ia.Board.Cards.Count);
        ia.PlayerTurn();
        Assert.Equal(++boardCount, ia.Board.Cards.Count);
    }
}