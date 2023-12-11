using System.Collections;
using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;

namespace LogicTest.datas_generators.score;

public class SushiScoreCalculatorDatasGenerator : IEnumerable<object[]>
{
    private CreatePlayerUtils createPlayerUtils = new();

    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new List<Player>
            {
                createPlayerUtils.CreatePlayer(0, new List<Card>
                {
                    new ChopstickCard(), new DessertCard(), new SushiCard(SushiTypes.SALMON)
                }),
                createPlayerUtils.CreatePlayer(1, new List<Card>
                {
                    new ChopstickCard(), new DessertCard(), new SushiCard(SushiTypes.SALMON),
                    new SushiCard(SushiTypes.SALMON), new SushiCard(SushiTypes.SALMON)
                })
            },
            new Dictionary<int, int>
            {
                { 0, 2 },
                { 1, 6 }
            }
        };
        yield return new object[]
        {
            new List<Player>
            {
                createPlayerUtils.CreatePlayer(1, new List<Card>
                {
                    new MakiCard(2), new SushiCard(SushiTypes.CALAMARI)
                }),
                createPlayerUtils.CreatePlayer(0, new List<Card>
                {
                    new MakiCard(1), new SushiCard(SushiTypes.CALAMARI), new SushiCard(SushiTypes.CALAMARI)
                })
            },
            new Dictionary<int, int>
            {
                { 1, 3 },
                { 0, 6 }
            }
        };
        yield return new object[]
        {
            new List<Player>
            {
                createPlayerUtils.CreatePlayer(0, new List<Card>
                {
                    new ChopstickCard(), new SushiCard(SushiTypes.OMELETTE), new SushiCard(SushiTypes.OMELETTE)
                }),
                createPlayerUtils.CreatePlayer(1, new List<Card>
                {
                    new ChopstickCard(), new SushiCard(SushiTypes.OMELETTE), new DessertCard()
                })
            },
            new Dictionary<int, int>
            {
                { 0, 2 },
                { 1, 1 }
            }
        };
        yield return new object[]
        {
            new List<Player>
            {
                createPlayerUtils.CreatePlayer(0, new List<Card>
                {
                    new ChopstickCard(), new SushiCard(SushiTypes.SALMON), new SushiCard(SushiTypes.CALAMARI)
                }),
                createPlayerUtils.CreatePlayer(1, new List<Card>
                {
                    new ChopstickCard(), new SushiCard(SushiTypes.OMELETTE), new SushiCard(SushiTypes.CALAMARI),
                    new SushiCard(SushiTypes.OMELETTE)
                }),
                createPlayerUtils.CreatePlayer(2, new List<Card>
                {
                    new ChopstickCard(), new SushiCard(SushiTypes.SALMON), new SushiCard(SushiTypes.SALMON),
                    new SushiCard(SushiTypes.SALMON), new DessertCard(), new SushiCard(SushiTypes.CALAMARI)
                })
            },
            new Dictionary<int, int>
            {
                { 0, 5 },
                { 1, 5 },
                { 2, 9 }
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}