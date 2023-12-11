using System.Collections;
using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;

namespace LogicTest.datas_generators.score;

public class TempuraScoreCalculatorDatasGenerator : IEnumerable<object[]>
{
    private readonly CreatePlayerUtils createPlayerUtils = new CreatePlayerUtils();

    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new List<Player>
            {
                createPlayerUtils.CreatePlayer(0, new List<Card>() { new TempuraCard(), new ChopstickCard(), new MakiCard(2) })
            },
            new Dictionary<int, int>
            {
                { 0, 0 }
            }
        };
        yield return new object[]
        {
            new List<Player>
            {
                createPlayerUtils.CreatePlayer(0, new List<Card> { new TempuraCard(), new TempuraCard() })
            },
            new Dictionary<int, int>
            {
                { 0, 5 }
            }
        };
        yield return new object[]
        {
            new List<Player>
            {
                createPlayerUtils.CreatePlayer(0,
                    new List<Card> { new TempuraCard(), new TempuraCard(), new TempuraCard(), new SashimiCard() })
            },
            new Dictionary<int, int>
            {
                { 0, 5 }
            }
        };
        yield return new object[]
        {
            new List<Player>
            {
                createPlayerUtils.CreatePlayer(0,
                    new List<Card> { new TempuraCard(), new TempuraCard(), new TempuraCard(), new TempuraCard() })
            },
            new Dictionary<int, int>
            {
                { 0, 10 }
            }
        };
        yield return new object[]
        {
            new List<Player>
            {
                createPlayerUtils.CreatePlayer(0,
                    new List<Card>
                    {
                        new TempuraCard(), new TempuraCard(), new TempuraCard(), new TempuraCard(), new TempuraCard()
                    }),
                createPlayerUtils.CreatePlayer(1,
                    new List<Card> { new TempuraCard(), new TempuraCard(), new TempuraCard(), new TempuraCard() }),
                createPlayerUtils.CreatePlayer(2, new List<Card> { new TempuraCard(), new TempuraCard(), new TempuraCard() }),
            },
            new Dictionary<int, int>
            {
                { 0, 10 },
                { 1, 10 },
                { 2, 5 }
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}