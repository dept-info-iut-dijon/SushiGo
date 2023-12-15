using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using System.Collections;

namespace LogicTest.datas_generators.score;

/// <summary>
/// Génère les jeux de données des cas à tester pour la méthode CalculateScore de la classe DessertScoreCalculator.
/// </summary>
public class DessertScoreCalculatorDatasGenerator : IEnumerable<object[]>
{
    private readonly CreatePlayerUtils createPlayerUtils = new CreatePlayerUtils();

    private Player createPlayer(int id, List<Card> cards)
    {
        return createPlayerUtils.CreatePlayer(id, cards);
    }

    /// <summary>
    /// Génère les jeux de données un à un
    /// </summary>
    /// <returns>Le jeu de données courant</returns>
    public IEnumerator<object[]> GetEnumerator()
    {
        // cas simple
        yield return new object[]
        {
            new List<Player>
            {
                createPlayer(0, new List<Card>()
                {
                    new DessertCard(), new DessertCard(), new DessertCard(), new ChopstickCard(), new MakiCard(2),
                    new RavioliCard()
                }),
                createPlayer(1, new List<Card>()
                {
                    new ChopstickCard(), new MakiCard(2), new DessertCard(), new RavioliCard()
                }),
                createPlayer(2, new List<Card>()
                {
                    new ChopstickCard(), new SushiCard(SushiTypes.SALMON), new SushiCard(SushiTypes.SALMON)
                })
            },
            new Dictionary<int, int>
            {
                {0, 6},
                {1, 0},
                {2, -6}
            }
        };
        // cas où on a deux joueurs en égalité sur le max de desserts
        yield return new object[]
        {
            new List<Player>()
            {
                createPlayer(0, new List<Card>()
                {
                    new DessertCard(), new DessertCard()
                }),
                createPlayer(1, new List<Card>()
                {
                    new DessertCard(), new DessertCard()
                }),
                createPlayer(2, new List<Card>()
                {
                    new DessertCard(), new MakiCard(3)
                })
            },
            new Dictionary<int, int>
            {
                { 0, 3 },
                { 1, 3 },
                { 2, -6 }
            }
        };

        // cas avec deux joueurs en égalité sur le min de desserts
        yield return new object[]
        {
            new List<Player>()
            {
                createPlayer(0, new List<Card>()
                {
                    new DessertCard()
                }),
                createPlayer(1, new List<Card>()
                {
                    new DessertCard()
                }),
                createPlayer(2, new List<Card>()
                {
                    new DessertCard(), new MakiCard(3), new DessertCard(), new DessertCard()
                }),
                createPlayer(3, new List<Card>()
                {
                    new DessertCard(), new MakiCard(3), new DessertCard()
                })
            },
            new Dictionary<int, int>
            {
                { 0, -3 },
                { 1, -3 },
                { 2, 6 },
                { 3, 0 }
            }
        };

        // cas avec deux joueurs en égalité sur le min et le max de desserts
        yield return new object[]
        {
            new List<Player>()
            {
                createPlayer(0, new List<Card>()
                {
                    new DessertCard()
                }),
                createPlayer(1, new List<Card>()
                {
                    new DessertCard()
                }),
                createPlayer(2, new List<Card>()
                {
                    new DessertCard(), new MakiCard(3), new DessertCard(), new DessertCard()
                }),
                createPlayer(3, new List<Card>()
                {
                    new DessertCard(), new MakiCard(3), new DessertCard()
                }),
                createPlayer(4, new List<Card>()
                {
                    new DessertCard(), new MakiCard(3), new DessertCard(), new DessertCard()
                })
            },
            new Dictionary<int, int>
            {
                { 0, -3 },
                { 1, -3 },
                { 2, 3 },
                { 3, 0 },
                { 4, 3 }
            }
        };

        // cas où les joueurs max et min de desserts sont les mêmes
        yield return new object[]
        {
            new List<Player>()
            {
                createPlayer(0, new List<Card>()
                {
                    new DessertCard(), new DessertCard(), new DessertCard()
                }),
                createPlayer(1, new List<Card>()
                {
                    new DessertCard(), new DessertCard(), new DessertCard()
                }),
                createPlayer(2, new List<Card>()
                {
                    new DessertCard(), new MakiCard(3), new DessertCard(), new DessertCard()
                }),
                createPlayer(3, new List<Card>()
                {
                    new DessertCard(), new MakiCard(3), new DessertCard(), new DessertCard()
                })
            },
            new Dictionary<int, int>
            {
                { 0, 0 },
                { 1, 0 },
                { 2, 0 },
                { 3, 0 }
            }
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}