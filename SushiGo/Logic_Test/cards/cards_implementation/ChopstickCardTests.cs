using Logic_Layer.cards.cards_implementation;

namespace LogicTest.cards.cards_implementation;

public class ChopstickCardTests
{
    [Fact]
    public void ChopstickCard_NameIsCorrect()
    {
        // Arrange
        var chopstickCard = new ChopstickCard();

        // Act
        string name = chopstickCard.Name;

        // Assert
        Assert.Equal("Baguettes", name);
    }

    [Fact]
    public void ChopstickCard_Activate_MakesCardUnavailable()
    {
        // Arrange
        var chopstickCard = new ChopstickCard();

        // Act
        chopstickCard.Activate();

        // Assert
        Assert.False(chopstickCard.PlayerTurn());
    }

    [Fact]
    public void ChopstickCard_PlayerTurn_ReturnsTrueWhenAvailable()
    {
        // Arrange
        var chopstickCard = new ChopstickCard();

        // Act
        bool result = chopstickCard.PlayerTurn();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void ChopstickCard_PlayerTurn_ReturnsFalseWhenUnavailable()
    {
        // Arrange
        var chopstickCard = new ChopstickCard();
        chopstickCard.Activate();

        // Act
        bool result = chopstickCard.PlayerTurn();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ChopstickCard_EndRound_ReturnsFalse()
    {
        // Arrange
        var chopstickCard = new ChopstickCard();

        // Act
        bool result = chopstickCard.EndRound();

        // Assert
        Assert.False(result);
    }
}
