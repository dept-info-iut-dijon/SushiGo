using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using System.Collections;

namespace LogicTest.datas_generators.score;

public class SashimiScoreCalculatorDatasGenerator : IEnumerable<object[]>
{
    private readonly CreatePlayerUtils createPlayerUtils = new CreatePlayerUtils();

    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new List<Player>
            {
                createPlayerUtils.CreatePlayer(0, new List<Card>())
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
                createPlayerUtils.CreatePlayer(0,
                    new List<Card>() { new ChopstickCard(), new DessertCard(), new SashimiCard(), new SashimiCard() })
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
                createPlayerUtils.CreatePlayer(0,
                    new List<Card>() { new ChopstickCard(), new SashimiCard(), new SashimiCard(), new SashimiCard() })
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
                    new List<Card>()
                    {
                        new ChopstickCard(), new SashimiCard(), new SashimiCard(), new SashimiCard(), new SashimiCard()
                    })
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
                    new List<Card>()
                    {
                        new ChopstickCard(), new SashimiCard(), new SashimiCard(), new SashimiCard(), new SashimiCard(),
                        new SashimiCard(), new SushiCard(SushiTypes.OMELETTE), new SashimiCard()
                    })
            },
            new Dictionary<int, int>
            {
                { 0, 20 }
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}