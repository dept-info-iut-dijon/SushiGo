using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using Logic_Layer.logic_exceptions;
using Moq;

namespace LogicTest;

public class HandTests
{
    [Fact]
    public void Hand_IdProperty_ReturnsCorrectValue()
    {
        // Arrange
        int expectedId = 1;
        var hand = new Hand(expectedId, new List<Card>());

        // Act
        int id = hand.Id;

        // Assert
        Assert.Equal(expectedId, id);
    }

    [Fact]
    public void Hand_CardsProperty_ReturnsCorrectList()
    {
        // Arrange
        var card1 = new Mock<Card>().Object;
        var card2 = new Mock<Card>().Object;
        var card3 = new Mock<Card>().Object;
        var cards = new List<Card> { card1, card2, card3 };
        var hand = new Hand(1, cards);

        // Act
        List<Card> result = hand.Cards;

        // Assert
        Assert.Equal(cards, result);
    }

    [Fact]
    public void Hand_PlayCard_CardNotInHandThrowsException()
    {
        // Arrange
        var hand = new Hand(1, new List<Card>());
        var board = new Board(); // Vous devrez peut-être créer un mock pour Board
        var cardToPlay = new Mock<Card>().Object; // Un mock pour une carte qui n'est pas dans la main

        // Act and Assert
        Assert.Throws<CardNotInHandException>(() => hand.PlayCard(cardToPlay, board));
    }

    [Fact]
    public void Hand_PlayCard_CardInHandPlaysCard()
    {
        // Arrange
        var cardInHand = new Mock<Card>().Object; // Un mock pour une carte dans la main
        var hand = new Hand(1, new List<Card> { cardInHand });
        var board = new Board(); // Vous devrez peut-être créer un mock pour Board

        // Act
        hand.PlayCard(cardInHand, board);

        // Assert
        Assert.Empty(hand.Cards);
        // Vous devrez peut-être vérifier si la carte a été correctement ajoutée au board (utilisez des mocks pour le board).
    }
    
    [Fact]
    public void PlayCard_TwoCardsInHandPlaysTwoCards()
    {
        // Arrange
        var cardInHand1 = new Mock<Card>().Object; // Un mock pour une carte dans la main
        var cardInHand2 = new Mock<Card>().Object; // Un mock pour une carte dans la main
        var hand = new Hand(1, new List<Card> { cardInHand1, cardInHand2 });
        
        var boardMock = new Mock<Board>();
        boardMock.Setup(x => x.CanPlayTwoCards).Returns(true);
        var board = boardMock.Object;

        // Act
        hand.PlayCard(cardInHand1, cardInHand2, board);

        // Assert
        Assert.Collection(hand.Cards, card => Assert.Equal(new ChopstickCard(), card));
        Assert.Single(hand.Cards);
        boardMock.Verify(x => x.AddCard(cardInHand1), Times.Once);
        boardMock.Verify(x => x.AddCard(cardInHand2), Times.Once);
        boardMock.Verify(x => x.PlayChopstickCard(), Times.Once);
    }
    
    [Fact]
    public void PlayCard_TwoCardsInHandPlaysTwoCards_ThrowsExceptionIfBoardCannotPlayTwoCards()
    {
        // Arrange
        var cardInHand1 = new Mock<Card>().Object; // Un mock pour une carte dans la main
        var cardInHand2 = new Mock<Card>().Object; // Un mock pour une carte dans la main
        var hand = new Hand(1, new List<Card> { cardInHand1, cardInHand2 });
        
        var boardMock = new Mock<Board>();
        boardMock.Setup(x => x.CanPlayTwoCards).Returns(false);
        var board = boardMock.Object;

        // Act and Assert
        Assert.Throws<NoChopstickInBoardException>(() => hand.PlayCard(cardInHand1, cardInHand2, board));
    }

    [Fact]
    public void PlayCard_PlayTwoCards_ThrowExceptionWhenCardsAreInvalid()
    {
        // Arrange
        var cardInHand1 = new Mock<Card>().Object; // Un mock pour une carte dans la main
        var cardInHand2 = new Mock<Card>().Object; // Un mock pour une carte dans la main
        var hand = new Hand(1, new List<Card> { cardInHand1, cardInHand2 });
        
        var boardMock = new Mock<Board>();
        boardMock.Setup(x => x.CanPlayTwoCards).Returns(true);
        var board = boardMock.Object;
        
        boardMock.Verify(x => x.PlayChopstickCard(), Times.Never);
        boardMock.Verify(x => x.AddCard(It.IsAny<Card>()), Times.Never);
        
        boardMock.Verify(x => x.PlayChopstickCard(), Times.Never);
        boardMock.Verify(x => x.AddCard(It.IsAny<Card>()), Times.Never);

        Assert.Throws<CardNotInHandException>(() => hand.PlayCard(cardInHand1, null, board));
        Assert.Throws<CardNotInHandException>(() => hand.PlayCard(null, cardInHand2, board));
    }
    
}
