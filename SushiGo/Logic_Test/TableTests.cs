using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.logic_exceptions;
using Moq;
using System.Reflection;
using System.Text;

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

        // Assert
        // Assurez-vous que NextRound a été appelé (vérifiez l'état interne de votre classe)
        // et que CurrentPlayer est maintenant le joueur suivant.
        // Par exemple : Assert.Equal(1, table.CurrentPlayerIndex);
    }
}