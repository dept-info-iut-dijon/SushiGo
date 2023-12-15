using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using System.Collections;

namespace LogicTest.datas_generators.score;

public class RavioliScoreCalculatorDatasGenerator : IEnumerable<object[]>
{
    private CreatePlayerUtils createPlayerUtils = new CreatePlayerUtils();

    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new List<Player>()
                { createPlayerUtils.CreatePlayer(0, new List<Card> { new ChopstickCard(), new MakiCard(1) }) },
            new Dictionary<int, int> { { 0, 0 } }
        };
        yield return new object[]
        {
            new List<Player>
            {
                createPlayerUtils.CreatePlayer(0,
                    new List<Card> { new ChopstickCard(), new MakiCard(1), new RavioliCard() })
            },
            new Dictionary<int, int> { { 0, 1 } }
        };
        yield return new object[]
        {
            new List<Player>
            {
                createPlayerUtils.CreatePlayer(0,
                    new List<Card> { new ChopstickCard(), new RavioliCard(), new MakiCard(3), new RavioliCard() })
            },
            new Dictionary<int, int> { { 0, 3 } }
        };
        yield return new object[]
        {
            new List<Player>
            {
                createPlayerUtils.CreatePlayer(0,
                    new List<Card>
                    {
                        new ChopstickCard(), new RavioliCard(), new MakiCard(3), new RavioliCard(), new RavioliCard()
                    })
            },
            new Dictionary<int, int> { { 0, 6 } }
        };
        yield return new object[]
        {
            new List<Player>()
            {
                createPlayerUtils.CreatePlayer(0,
                    new List<Card>
                    {
                        new ChopstickCard(), new RavioliCard(), new MakiCard(3), new RavioliCard(), new RavioliCard(),
                        new RavioliCard()
                    })
            },
            new Dictionary<int, int> { { 0, 10 } }
        };
        yield return new object[]
        {
            new List<Player>()
            {
                createPlayerUtils.CreatePlayer(0,
                    new List<Card>
                    {
                        new ChopstickCard(), new RavioliCard(), new MakiCard(3), new RavioliCard(), new RavioliCard(),
                        new RavioliCard(), new RavioliCard()
                    })
            },
            new Dictionary<int, int> { { 0, 15 } }
        };
        yield return new object[]
        {
            new List<Player>()
            {
                createPlayerUtils.CreatePlayer(0,
                    new List<Card>
                    {
                        new ChopstickCard(), new RavioliCard(), new MakiCard(3), new RavioliCard(), new RavioliCard(),
                        new RavioliCard(), new RavioliCard(), new RavioliCard()
                    })
            },
            new Dictionary<int, int> { { 0, 15 } }
        };
        yield return new object[]
        {
            new List<Player>()
            {
                createPlayerUtils.CreatePlayer(0,
                    new List<Card>
                    {
                        new ChopstickCard(), new RavioliCard(), new MakiCard(3), new RavioliCard(), new RavioliCard(),
                        new RavioliCard(), new RavioliCard(), new RavioliCard(), new RavioliCard()
                    })
            },
            new Dictionary<int, int> { { 0, 15 } }
        };
        yield return new object[]
        {
            new List<Player>()
            {
                createPlayerUtils.CreatePlayer(0,
                    new List<Card>
                    {
                        new ChopstickCard(), new RavioliCard(), new MakiCard(3), new RavioliCard(), new RavioliCard()
                    }
                ),
                createPlayerUtils.CreatePlayer(1,
                    new List<Card>
                    {
                        new MakiCard(2), new RavioliCard(), new SushiCard(SushiTypes.SALMON), new RavioliCard()
                    }
                ),
                createPlayerUtils.CreatePlayer(2,
                    new List<Card>
                    {
                        new MakiCard(1), new RavioliCard(), new SushiCard(SushiTypes.SALMON), new RavioliCard(),
                        new RavioliCard(), new RavioliCard(), new SashimiCard()
                    }
                )
            },
            new Dictionary<int, int>
            {
                { 0, 6 },
                { 1, 3 },
                { 2, 10 }
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}