using Logic_Layer;
using Logic_Layer.cards;

namespace LogicTest.datas_generators.score;

public class CreatePlayerUtils
{
    public Player CreatePlayer(int id, List<Card> cards)
    {
        var ret = new Player(id, new Board(), null, null);
        foreach (var card in cards)
        {
            ret.Board.AddCard(card);
        }

        return ret;
    }
}