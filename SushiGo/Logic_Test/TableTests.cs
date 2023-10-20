using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.logic_exceptions;
using Moq;

namespace LogicTest;

public class TableTests
{
    [Fact]
public void Table_InitializeWithValidPlayersNumber_CreatesPlayers()
{
    // Arrange
    int playersNumber = 4;
    var table = new Table(playersNumber);

    // Act and Assert
    Assert.Equal(playersNumber, table.Players.Count);
}

[Fact]
public void Table_InitializeWithInvalidPlayersNumber_ThrowsException()
{
    // Arrange
    int invalidPlayersNumber = 6; // Remplacez par un nombre de joueurs invalide
    // Act and Assert
    var exception = Assert.Throws<WrongPlayersNumberException>(() => new Table(invalidPlayersNumber));

    // Vérifiez le message d'exception (facultatif)
    Assert.Equal("Le nombre de joueur doit être inclus entre 2 et 5", exception.Message);
}

[Fact]
public void Table_NextPlayerTurn_NoMoreCards_StartsNextRound()
{
    // Arrange
    var table = new Table(4); // Remplacez par le nombre de joueurs souhaité
    var player = table.Players[0];

    // Act
    player.Hand.Cards.Clear(); // Simulez l'absence de cartes dans la main du joueur
    table.NextPlayerTurn();

    // Assert
    // Assurez-vous que NextRound a été appelé (vérifiez l'état interne de votre classe)
    // et que CurrentPlayer est maintenant le joueur suivant.
    // Par exemple : Assert.Equal(1, table.CurrentPlayerIndex);
}

[Fact]
public void Table_PlayCard_PlayerNotInGame_ThrowsException()
{
    // Arrange
    var table = new Table(4); // Remplacez par le nombre de joueurs souhaité
    var player = new Player(999, new Board(), new Hand(999, new List<Card>()));
    var cardMock = new Mock<Card>();
    var card = cardMock.Object;

    // Act and Assert
    var exception = Assert.Throws<PlayerImpossibleToFindException>(() => table.PlayCard(player, card));

    // Vérifiez le message d'exception (facultatif)
    // Assurez-vous que le message d'exception indique que le joueur n'est pas en jeu.
    // Par exemple : Assert.Equal("Le joueur n'est pas dans la partie", exception.Message);
}

}