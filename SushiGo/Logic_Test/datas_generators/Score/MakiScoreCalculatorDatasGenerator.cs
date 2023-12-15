using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using System.Collections;

namespace LogicTest.datas_generators.score;

public class MakiScoreCalculatorDatasGenerator : IEnumerable<object[]>
{
    private CreatePlayerUtils createPlayerUtils = new CreatePlayerUtils();

    private Player createPlayer(int id, List<Card> cards)
    {
        return createPlayerUtils.CreatePlayer(id, cards);
    }

    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            new List<Player>
            {
                createPlayer(0, new List<Card>
                {
                    new MakiCard(1)
                }),
                createPlayer(1, new List<Card>
                {
                    new SushiCard(SushiTypes.SALMON)
                })
            },
            new Dictionary<int, int>
            {
                {0, 6},
                {1, 0}
            }
        };
        yield return new object[]
        {
            new List<Player>()
            {
                createPlayer(0, new List<Card>()
                {
                    new MakiCard(1), new MakiCard(1)
                }),
                createPlayer(1, new List<Card>()
                {
                    new MakiCard(1)
                }),
                createPlayer(2, new List<Card>()
                {
                    new SashimiCard()
                })
            },
            new Dictionary<int, int>
            {
                { 0, 6 },
                { 1, 3 },
                { 2, 0 }
            }
        };
        // Exemple donné dans les règles en BD, parce que pourquoi pas
        yield return new object[]
        {
            new List<Player>()
            {
                createPlayer(0, new List<Card>()
                {
                    new MakiCard(2), new MakiCard(3)
                }),
                createPlayer(1, new List<Card>()
                {
                    new MakiCard(1), new SashimiCard(), new MakiCard(2)
                }),
                createPlayer(2, new List<Card>()
                {
                    new MakiCard(3), new TempuraCard()
                }),
                createPlayer(3, new List<Card>()
                {
                    new MakiCard(1), new MakiCard(1), new DessertCard()
                })
            },
            new Dictionary<int, int>
            {
                { 0, 6 },
                { 1, 1 },
                { 2, 1 },
                { 3, 0 }
            }
        };
        yield return new object[]
        {
            new List<Player>()
            {
                createPlayer(0, new List<Card>()
                {
                    new MakiCard(3), new MakiCard(1), new MakiCard(2)
                }),
                createPlayer(1, new List<Card>()
                {
                    new MakiCard(1), new SashimiCard()
                }),
                createPlayer(2, new List<Card>()
                {
                    new MakiCard(3), new MakiCard(3)
                }),
                createPlayer(3, new List<Card>()
                {
                    new DessertCard(), new TempuraCard()
                })
            },
            new Dictionary<int, int>
            {
                { 0, 3 },
                { 1, 0 },
                { 2, 3 },
                { 3, 0 }
            }
        };
        yield return new object[]
        {
            new List<Player>()
            {
                createPlayer(0, new List<Card>()
                {
                    new MakiCard(2), new ChopstickCard(), new MakiCard(1)
                }),
                createPlayer(1, new List<Card>()
                {
                    new MakiCard(1), new SashimiCard(), new MakiCard(2)
                }),
                createPlayer(2, new List<Card>()
                {
                    new MakiCard(3)
                }),
                createPlayer(3, new List<Card>()
                {
                    new MakiCard(2), new MakiCard(1), new TempuraCard()
                }),
                createPlayer(4, new List<Card>()
                {
                    new MakiCard(1), new TempuraCard()
                })
            },
            new Dictionary<int, int>
            {
                { 0, 1 },
                { 1, 1 },
                { 2, 1 },
                { 3, 1 },
                { 4, 0 }
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}