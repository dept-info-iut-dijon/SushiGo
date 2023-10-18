using Logic_Layer;
using Logic_Layer.cards;
using Moq;

namespace LogicTest;

public class PlayerTests
{
    [Fact]
    public void Player_IdProperty_ReturnsCorrectValue()
    {
        // Arrange
        int expectedId = 1;
        var player = new Player(expectedId, new Board(), new Hand(expectedId, new List<Card>()));

        // Act
        int id = player.Id;

        // Assert
        Assert.Equal(expectedId, id);
    }

    [Fact]
    public void Player_HaveCardsProperty_InitialValueIsTrue()
    {
        // Arrange
        var player = new Player(1, new Board(), new Hand(1, new List<Card> { new Mock<Card>().Object }));

        // Act
        bool haveCards = player.HaveCards;

        // Assert
        Assert.True(haveCards);
    }

    [Fact]
    public void Player_HaveCardsProperty_NoCardsInHandReturnsFalse()
    {
        // Arrange
        var player = new Player(1, new Board(), new Hand(1, new List<Card>()));

        // Act
        bool haveCards = player.HaveCards;

        // Assert
        Assert.False(haveCards);
    }

    [Fact]
    public void Player_PlayCard_CallsHandPlayCard()
    {
        // Arrange
        var card = new Mock<Card>().Object;
        var cardList = new List<Card>();
        cardList.Add(card);
        var handMock = new Mock<Hand>(1, cardList);
        var player = new Player(1, new Board(), handMock.Object);

        // Act
        player.PlayCard(card);

        // Assert
        handMock.Verify(hand => hand.PlayCard(card, It.IsAny<Board>()), Times.Once);
    }

    [Fact]
    public void Player_PlayerTurn_ReturnsSpecialCardsFromBoard()
    {
        // Arrange
        var boardMock = new Mock<Board>();
        var player = new Player(1, boardMock.Object, new Hand(1, new List<Card>()));

        var specialCards = new List<ISpecialCard> { new Mock<ISpecialCard>().Object };
        boardMock.Setup(board => board.PlayerTurn()).Returns(specialCards);

        // Act
        var result = player.PlayerTurn();

        // Assert
        Assert.Equal(specialCards, result);
    }

    [Fact]
    public void Player_EndTurn_ReturnsSpecialCardsFromBoard()
    {
        // Arrange
        var boardMock = new Mock<Board>();
        var player = new Player(1, boardMock.Object, new Hand(1, new List<Card>()));

        var specialCards = new List<ISpecialCard> { new Mock<ISpecialCard>().Object };
        boardMock.Setup(board => board.EndTurn()).Returns(specialCards);

        // Act
        var result = player.EndTurn();

        // Assert
        Assert.Equal(specialCards, result);
    }
}
