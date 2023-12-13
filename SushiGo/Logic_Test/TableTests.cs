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

    [Fact]
    public void NewHand_WhenNewRound()
    {
        // Arrange
        var handMock = new Mock<Hand>(It.IsAny<int>(), It.IsAny<List<Card>>());

        handMock.Setup(hand => hand.Cards).Returns(new List<Card>());


        var boardMock = new Mock<Board>();

        boardMock.Setup(board => board.Cards).Returns(new List<Card>());

        boardMock.Setup(board => board.EndRound()).Returns(new List<ISpecialCard>());


        var playerMock1 = new Mock<Player>(It.IsAny<int>(), It.IsAny<Board>(), It.IsAny<Hand>(), It.IsAny<string>());
        var playerMock2 = new Mock<Player>(It.IsAny<int>(), It.IsAny<Board>(), It.IsAny<Hand>(), It.IsAny<string>());

        playerMock1.Setup(bob => bob.Hand).Returns(handMock.Object);
        playerMock2.Setup(bob => bob.Hand).Returns(handMock.Object);

        playerMock1.Setup(bob => bob.Board).Returns(boardMock.Object);
        playerMock2.Setup(bob => bob.Board).Returns(boardMock.Object);

        playerMock1.Setup(bob => bob.PlayerTurn());
        playerMock2.Setup(bob => bob.PlayerTurn());

        playerMock1.Setup(bob => bob.EndRound());
        playerMock2.Setup(bob => bob.EndRound());
        

        var table = new Table(new List<Player>() { playerMock1.Object, playerMock2.Object });


        // Act
        Type type = typeof(Table);
        MethodInfo methodInfo = type.GetMethod("NextRound", BindingFlags.NonPublic | BindingFlags.Instance);

        methodInfo.Invoke(table, null);


        // Assert
        playerMock1.VerifySet(bob => bob.Hand, Times.Exactly(2));
    }
}