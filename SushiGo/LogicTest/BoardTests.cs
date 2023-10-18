using Logic_Layer;
using Logic_Layer.cards;

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
        var result = board.EndTurn();

        // Assert
        Assert.Contains(specialCard1, result);
        Assert.DoesNotContain(specialCard2, result);
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