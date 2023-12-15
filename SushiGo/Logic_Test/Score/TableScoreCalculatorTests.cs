using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.score;
using Moq;
using System.Reflection;

namespace LogicTest.Score;

public class TableScoreCalculatorTests
{
    [Fact]
    public void CalculateScore_DoesntTakeDessertIntoAccount_WhenItIsNotLastRound()
    {
        var playersList = new List<Player>()
        {
            new Player(0, new Board(), new Hand(0, new List<Card>()), "pseudo"),
            new Player(1, new Board(), new Hand(1, new List<Card>()), "pseudo"),
        };
        var calculator = new TableScoreCalculator(new Table(playersList));

        // on récupère l'attribut à mocker
        FieldInfo? dessertCalculatorInfo = typeof(TableScoreCalculator).GetField("endGameScoreCalculators",
            BindingFlags.NonPublic | BindingFlags.Instance);

        // on crée  le mock
        Mock<DessertScoreCalculator> mockedDessertCalculator = new Mock<DessertScoreCalculator>();

        // on insère le mock dans l'attribut
        dessertCalculatorInfo?.SetValue(calculator, new List<IScoreCalculator>()
        {
            (IScoreCalculator) mockedDessertCalculator.Object
        });

        calculator.CalculateScore();

        mockedDessertCalculator.Verify(x => x.CalculateScore(playersList), Times.Never());

    }

    [Fact]
    public void CalculateScore_TakeDessertIntoAccount_WhenItIsLastRound()
    {
        var playersList = new List<Player>()
        {
            new Player(0, new Board(), new Hand(0, new List<Card>()), "pseudo"),
            new Player(1, new Board(), new Hand(1, new List<Card>()), "pseudo"),
        };

        var table = new Table(playersList);
        playersList = table.Players;
        var calculator = new TableScoreCalculator(table);

        // On passe le jeu au dernier round
        while (table.RoundNumber < 3)
        {
            foreach (var p in table.Players)
            {
                if (p.Hand.Cards.Count > 0)
                    p.PlayCard(p.Hand.Cards.First());
            }
            table.NextTurn();
        }

        // on récupère l'attribut à mocker
        FieldInfo? dessertCalculatorInfo = typeof(TableScoreCalculator).GetField("endGameScoreCalculators",
            BindingFlags.NonPublic | BindingFlags.Instance);

        // on crée  le mock
        Mock<DessertScoreCalculator> mockedDessertCalculator = new Mock<DessertScoreCalculator>();
        mockedDessertCalculator.Setup(x => x.CalculateScore(It.IsAny<List<Player>>())).Returns(new Dictionary<int, int>()
        {
            {0, 10},
            {1, 10}
        });

        // on insère le mock dans l'attribut
        dessertCalculatorInfo?.SetValue(calculator, new List<IScoreCalculator>()
        {
            (IScoreCalculator) mockedDessertCalculator.Object
        });

        calculator.CalculateScore();

        mockedDessertCalculator.Verify(x => x.CalculateScore(playersList));
    }
}