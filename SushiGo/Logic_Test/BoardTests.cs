using Logic_Layer;
using Logic_Layer.cards;
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
        for (int i = 0; i < 5; i++)
        {
            var card = new Mock<Card>();
            card.Setup(c => c.Name).Returns("Card " + i);
            cards.Add(card.Object);
        }
        
        // Create a board and add the cards
        var board = new Board();
        foreach (var card in cards)
        {
            board.AddCard(card);
        }
        var specialCard = new SpecialCardWithTrueEndRound();
        board.AddCard(specialCard);
        board.AddCard(new SpecialCardWithTruePlayerTurn());
        
        var boardCardList = board.Cards;
        var toRemove = new List<Card>();

        foreach (var card in boardCardList)
        {
            if(card is ISpecialCard c && c.EndRound())
                toRemove.Add(card);
        }

        foreach (var card in toRemove)
        {
            boardCardList.Remove(card);
        }
        
        Assert.NotEmpty(board.Cards);
        Assert.NotEmpty(boardCardList);
        
        board.EndRound();
        boardCardList = board.Cards;

        foreach (var card in boardCardList)
        {
            if(card is ISpecialCard c && c.EndRound())
                toRemove.Add(card);
        }

        foreach (var card in toRemove)
        {
            boardCardList.Remove(card);
        }
        
        Assert.Empty(boardCardList);
        Assert.NotEmpty(board.Cards);
    }
}

/*          CLASSES MOCK        */
public class SpecialCardWithTruePlayerTurn : Card, ISpecialCard
{
    public bool PlayerTurn()
    {
        return true;
    }

    public bool EndRound()
    {
        return false;
    }

    public override string Name => throw new NotImplementedException();
}

public class SpecialCardWithFalsePlayerTurn : Card, ISpecialCard
{
    public bool PlayerTurn()
    {
        return false;
    }

    public bool EndRound()
    {
        return false;
    }

    public override string Name => throw new NotImplementedException();
}

public class SpecialCardWithTrueEndRound : Card, ISpecialCard
{
    public bool PlayerTurn()
    {
        return false;
    }

    public bool EndRound()
    {
        return true;
    }

    public override string Name => throw new NotImplementedException();
}

public class SpecialCardWithFalseEndRound : Card, ISpecialCard
{
    public bool PlayerTurn()
    {
        return false;
    }

    public bool EndRound()
    {
        return false;
    }

    public override string Name => throw new NotImplementedException();
}