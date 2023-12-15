using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using System.Collections;

namespace LogicTest.datas_generators
{
    internal class IAFactoryDatasGenerator : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                2, new Board(), new Hand(3, new List<Card>())
            };

            // Création d'un Board avec une carte de chaque type
            Board board = new Board();
            board.AddCard(new ChopstickCard());
            board.AddCard(new MakiCard(2));
            board.AddCard(new SushiCard(SushiTypes.CALAMARI));
            board.AddCard(new TempuraCard());
            board.AddCard(new WasabiCard());

            yield return new object[]
            {
                3, board, new Hand(3, new List<Card>())
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
