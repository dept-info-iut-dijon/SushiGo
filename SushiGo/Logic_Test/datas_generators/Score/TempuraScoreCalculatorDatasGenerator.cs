using System.Collections;
using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;

namespace LogicTest.datas_generators.Score;

public class TempuraScoreCalculatorDatasGenerator : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new List<Hand>
            {
                new Hand(0, new List<Card>())
            },
            new Dictionary<int, int>
            {
                {0, 0}
            }
        };
        yield return new object[]
        {
            new List<Hand>
            {
                new Hand(0, new List<Card> {new TempuraCard(), new TempuraCard()})
            },
            new Dictionary<int, int>
            {
                {0, 5}
            }
        };
        yield return new object[]
        {
            new List<Hand>
            {
                new Hand(0, new List<Card> {new TempuraCard(), new TempuraCard(), new TempuraCard()})
            },
            new Dictionary<int, int>
            {
                {0, 5}
            }
        };
        yield return new object[]
        {
            new List<Hand>
            {
                new Hand(0, new List<Card> {new TempuraCard(), new TempuraCard(), new TempuraCard(), new TempuraCard()})
            },
            new Dictionary<int, int>
            {
                {0, 10}
            }
        };
        yield return new object[]
        {
            new List<Hand>
            {
                new Hand(0, new List<Card> {new TempuraCard(), new TempuraCard(), new TempuraCard(), new TempuraCard(), new TempuraCard()}),
                new Hand(1, new List<Card> {new TempuraCard(), new TempuraCard(), new TempuraCard(), new TempuraCard()}),
                new Hand(2, new List<Card> {new TempuraCard(), new TempuraCard(), new TempuraCard()}),
            },
            new Dictionary<int, int>
            {
                {0, 10},
                {1, 10},
                {2, 5}
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}