using Logic_Layer;
using Logic_Layer.cards;
using Logic_Layer.cards.cards_implementation;
using Logic_Layer.logic_exceptions;
using Moq;

namespace LogicTest;

public class BoardTests
{
    [Fact]
    public void PlayerTurn_ReturnsSpecialCardsWithTruePlayerTurn()
    {
        // Arrange
        var board = new Board();
        var specialCard1 = new SpecialCardWithTruePlayerTurn();
        var specialCard2 = new SpecialCardWithFalsePlayerTurn();
        board.AddCard(specialCard1);
        board.AddCard(specialCard2);

        // Act
        var result = board.PlayerTurn();

        // Assert
        Assert.Contains(specialCard1, result);
        Assert.DoesNotContain(specialCard2, result);
    }

    [Fact]
    public void EndRound_ReturnsSpecialCardsWithTrueEndRound()
    {
        // Arrange
        var board = new Board();
        var specialCard1 = new SpecialCardWithTrueEndRound();
        var specialCard2 = new SpecialCardWithFalseEndRound();
        board.AddCard(specialCard1);
        board.AddCard(specialCard2);

        // Act
        var result = board.EndRound();

        // Assert
        Assert.Contains(specialCard1, result);
        Assert.DoesNotContain(specialCard2, result);
    }

    [Fact]
    public void EndRound_ClearCards()
    {
        // Create a list of 5 mock cards
        var cards = new List<Card>();
        for (var i = 0; i < 5; i++)
        {
            var card = new Mock<Card>();
            card.Setup(c => c.Name).Returns("Card " + i);
            cards.Add(card.Object);
        }

        // Create a board and add the cards
        var board = new Board();
        foreach (var card in cards) board.AddCard(card);
        var specialCard = new SpecialCardWithTrueEndRound();
        board.AddCard(specialCard);
        board.AddCard(new SpecialCardWithTruePlayerTurn());

        var boardCardList = board.Cards;
        var toRemove = new List<Card>();

        foreach (var card in boardCardList)
            if (!(card is ISpecialCard c && c.EndRound()))
                toRemove.Add(card);

        foreach (var card in toRemove) boardCardList.Remove(card);

        Assert.NotEmpty(board.Cards);
        Assert.NotEmpty(boardCardList);

        board.EndRound();
        boardCardList = board.Cards;

        foreach (var card in boardCardList)
            if (!(card is ISpecialCard c && c.EndRound()))
                toRemove.Add(card);

        foreach (var card in toRemove) boardCardList.Remove(card);

        Assert.NotEmpty(board.Cards);

        for (var index = 0; index < board.Cards.Count; index++)
        {
            var card = board.Cards[index];
            Assert.Equal(boardCardList[index], card);
        }
    }
    
    [Fact]
    public void PlayChopstickCard()
    {
        var board = new Board();
        var chopstickCard = new ChopstickCard();
        board.AddCard(chopstickCard);
        
        Assert.Contains(chopstickCard, board.Cards);
        
        board.PlayChopstickCard();
        
        Assert.DoesNotContain(chopstickCard, board.Cards);
    }

    [Fact]
    public void PlayChopstickCard_ThrowsException()
    {
        var board = new Board();

        Assert.Throws<NoChopstickInBoardException>(() => board.PlayChopstickCard());
    }
    
    [Fact]
    public void CanPlayTwoCards_False_WhenNoChopstick()
    {
        var board = new Board();
        var card = new Mock<Card>();
        card.Setup(c => c.Name).Returns("Card");
        board.AddCard(card.Object);
        
        Assert.False(board.CanPlayTwoCards);
    }

    [Fact]
    public void CanPlayTwoCards_True_WhenContainsChopstick()
    {
        var board = new Board();
        var chopstickCard = new ChopstickCard();
        board.AddCard(chopstickCard);
        
        Assert.True(board.CanPlayTwoCards);
    }
    
    [Fact]
    public void AddCard()
    {
        var board = new Board();
        var card = new Mock<Card>();
        card.Setup(c => c.Name).Returns("Card");
        
        Assert.DoesNotContain(card.Object, board.Cards);
        
        board.AddCard(card.Object);
        
        Assert.Contains(card.Object, board.Cards);
    }
}

/*          CLASSES MOCK        */
public class SpecialCardWithTruePlayerTurn : Card, ISpecialCard
{
    public override string Name => throw new NotImplementedException();

    public bool PlayerTurn()
    {
        return true;
    }

    public bool EndRound()
    {
        return false;
    }
}

public class SpecialCardWithFalsePlayerTurn : Card, ISpecialCard
{
    public override string Name => throw new NotImplementedException();

    public bool PlayerTurn()
    {
        return false;
    }

    public bool EndRound()
    {
        return false;
    }
}

public class SpecialCardWithTrueEndRound : Card, ISpecialCard
{
    public override string Name => throw new NotImplementedException();

    public bool PlayerTurn()
    {
        return false;
    }

    public bool EndRound()
    {
        return true;
    }
}

public class SpecialCardWithFalseEndRound : Card, ISpecialCard
{
    public override string Name => throw new NotImplementedException();

    public bool PlayerTurn()
    {
        return false;
    }

    public bool EndRound()
    {
        return false;
    }
}