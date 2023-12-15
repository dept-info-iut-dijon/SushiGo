using Logic_Layer;
using Logic_Layer.logic_exceptions;
using Moq;
using System.Reflection;
using System.Text;
using Logic_Layer.cards.cards_implementation;

namespace LogicTest;

public class TableTests
{
    [Fact]
    public void Table_InitializeWithValidPlayersNumber_CreatesPlayers()
    {
        // Arrange
        List<Player> players = new List<Player>()
        {
            new Player(1,new Board(),null,""),
            new Player(2,new Board(),null,""),
            new Player(3,new Board(),null,""),
            new Player(4,new Board(),null,""),
        };
        var table = new Table(players);

        // Act and Assert
        Assert.Equal(4, table.Players.Count);
    }

    [Fact]
    public void Table_InitializeWithInvalidPlayersNumber_ThrowsException()
    {
        // Arrange
        List<Player> players = new List<Player>()
        {
            new Player(1,new Board(),null,""),
            new Player(2,new Board(),null,""),
            new Player(3,new Board(),null,""),
            new Player(4,new Board(),null,""),
            new Player(5,new Board(),null,""),
            new Player(6,new Board(),null,""),
        };
        // Act and Assert
        var exception = Assert.Throws<WrongPlayersNumberException>(() => new Table(players));

        // Vérifiez le message d'exception (facultatif)
        Assert.Equal("Le nombre de joueur doit être inclus entre 2 et 5", exception.Message);
    }

    [Fact]
    public void Table_NextPlayerTurn_NoMoreCards_StartsNextRound()
    {
        // Arrange
        List<Player> players = new List<Player>()
        {
            new Player(1, new Board(), null, ""),
            new Player(2, new Board(), null, ""),
            new Player(3, new Board(), null, ""),
            new Player(4, new Board(), null, ""),
        };
        var table = new Table(players); // Remplacez par le nombre de joueurs souhaité
        var player = table.Players[0];

        // Act
        player.Hand.Cards.Clear(); // Simulez l'absence de cartes dans la main du joueur
        table.NextTurn();
    }

    [Fact]
    public void RotateHands_ChangesCorrectly_BasedOnRoundNumber_WhenGameOrderIsProgressive()
    {
        List<Player> players = new List<Player>()
        {
            new Player(0, new Board(), new Hand(0, new List<Card>() { new DessertCard() }), ""),
            new Player(1, new Board(), new Hand(1, new List<Card>() { new DessertCard() }), ""),
            new Player(2, new Board(), new Hand(2, new List<Card>() { new MakiCard(1) }), ""),
            new Player(3, new Board(), new Hand(3, new List<Card>() { new SashimiCard() }), ""),
        };
        var table = new Table(players);
        
        // Round 1
        List<Hand> nextHands = new List<Hand>()
        {
            players[3].Hand,
            players[0].Hand,
            players[1].Hand,
            players[2].Hand
        };
        
        Assert.Equal(GameOrderEnum.PROGRESSIVE, table.GameOrder);
        
        table.NextTurn();

        for (var index = 0; index < nextHands.Count; index++)
        {
            var hand = nextHands[index];
            Assert.Equal(hand, table.Players[index].Hand);
        }
    }
    
    [Fact]
    public void RotateHands_ChangesCorrectly_BasedOnRoundNumber_WhenGameOrderIsRegressive()
    {
        List<Player> players = new List<Player>()
        {
            new Player(0, new Board(), new Hand(0, new List<Card>() ), ""),
            new Player(1, new Board(), new Hand(1, new List<Card>() ), ""),
            new Player(2, new Board(), new Hand(2, new List<Card>()), ""),
            new Player(3, new Board(), new Hand(3, new List<Card>() ), ""),
        };
        var table = new Table(players);
        foreach (var player in table.Players)
        {
            player.Hand.Cards.Clear();
        }
        table.NextTurn();

        foreach (var player in table.Players)
        {
            player.Hand.Cards.Add(new SashimiCard());
        }
        
        // Round 2
        var nextHands = new List<Hand>()
        {
            players[1].Hand,
            players[2].Hand,
            players[3].Hand,
            players[0].Hand
        };
        
        Assert.Equal(GameOrderEnum.REGRESSIVE, table.GameOrder);
        
        table.NextTurn();
        
        for (var index = 0; index < nextHands.Count; index++)
        {
            var hand = nextHands[index];
            Assert.Equal(hand, table.Players[index].Hand);
        }
    }
}